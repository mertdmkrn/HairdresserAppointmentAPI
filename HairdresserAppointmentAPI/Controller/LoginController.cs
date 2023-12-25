using HairdresserAppointmentAPI.Handler.Abstract;
using HairdresserAppointmentAPI.Handler.Concrete;
using HairdresserAppointmentAPI.Handler.Model;
using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private IBusinessService _businessService;
        private ITokenHandler _tokenHandler;
        private readonly IMailHandler _mailHandler;

        public LoginController(IUserService userService, IBusinessService businessService, ITokenHandler tokenHandler, IMailHandler mailHandler)
        {
            _userService = userService;
            _businessService = businessService;
            _tokenHandler = tokenHandler;
            _mailHandler = mailHandler;
        }


        /// <summary>
        /// User Login Control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/login")]
        public async Task<IActionResult> UserLogin(string? email, string? password)
        {
            ResponseModel<TokenInfo<User>> response = new ResponseModel<TokenInfo<User>>();

            if (email.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add(new ValidationError("email", "Email boş bırakılmamalı."));
                response.Message += "Email boş bırakılmamalı.";
            }

            if (password.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add(new ValidationError("password", "Şifre boş bırakılmamalı."));
                response.Message += "Şifre boş bırakılmamalı.";
            }

            if (response.HasError)
                return BadRequest(response);

            var user = await _userService.GetUserByEmailAndPasswordAsync(email, password);

            if (user == null)
            {
                response.HasError = true;
                response.Message = "Girdiğiniz bilgilere ait kullanıcı bulunamadı.";
                return NotFound(response);
            }

            if(!user.verified)
            {
                response.HasError = true;
                response.Message = "Hesabınız onaylanmamış lütfen email adresinizi kontrol ediniz.";
                return NotFound(response);
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.fullName),
                new Claim(ClaimTypes.Email, user.email)
            };

            response.Data = new TokenInfo<User>();
            response.Data.token = _tokenHandler.CreateAccessToken(DateTime.Now.AddDays(1), claims);
            response.Data.userInfo = user;

            return Ok(response);
        }

        /// <summary>
        /// Business Login Control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("business/login")]
        public async Task<IActionResult> BusinessLogin(string? email, string? password)
        {
            ResponseModel<TokenInfo<Business>> response = new ResponseModel<TokenInfo<Business>>();

            if (email.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add(new ValidationError("email", "Email boş bırakılmamalı."));
                response.Message += "Email boş bırakılmamalı.";
            }

            if (password.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add(new ValidationError("password", "Şifre boş bırakılmamalı."));
                response.Message += "Şifre boş bırakılmamalı.";
            }

            if (response.HasError)
                return BadRequest(response);

            var business = await _businessService.GetBusinessByEmailAndPasswordAsync(email, password);

            if (business == null)
            {
                response.HasError = true;
                response.Message = "Girdiğiniz bilgilere ait işletme bulunamadı.";
                return NotFound(response);
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, response.Data.userInfo.name),
                new Claim(ClaimTypes.Email, response.Data.userInfo.email)
            };

            response.Data = new TokenInfo<Business>();
            response.Data.token = _tokenHandler.CreateAccessToken(DateTime.Now.AddDays(1), claims);
            response.Data.userInfo = business;

            return Ok(response);
        }

        /// <summary>
        /// User Confirm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/confirm")]
        public async Task<IActionResult> UserConfirm(string? key)
        {
            ResponseModel<User> response = new ResponseModel<User>();

            if (key.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add(new ValidationError("key", "Key boş bırakılmamalı."));
                response.Message += "Key boş bırakılmamalı.";
            }

            if (response.HasError)
                return BadRequest(response);

            var keyArr = key.Split(".");
            var keyPassword = key.Replace("." + keyArr.LastOrDefault(), "").Replace("~", "=").Replace(" ", "+");

            var user = await _userService.GetUserById(keyArr.LastOrDefault().ToInt());

            if (user == null)
            {
                response.HasError = true;
                response.Message = "Böyle bir kullanıcı bulunamadı.";
                return NotFound(response);
            }

            if (user.password != keyPassword)
            {
                response.HasError = true;
                response.Message = "Böyle bir onay linki bulunamadı.";
                return NotFound(response);
            }

            user.verified = true;
            response.Data = await _userService.UpdateUserAsync(user);

            response.Message = "Kullanıcı onaylanmıştır.";

            return Ok(response);
        }


        /// <summary>
        /// Create New User
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "firstName": "Mert",
        ///        "lastName": "DEMİRKIRAN",
        ///        "email": "mertdmkrn37@gmail.com",
        ///        "password": "stms5581",
        ///        "imagePath": "images/mertdemirkiran.jpg"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("user/save")]
        public async Task<IActionResult> Save([FromBody] User user)
        {
            ResponseModel<User> response = new ResponseModel<User>();
            try
            {
                if (user.email.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("email", "Email boş bırakılmamalı."));
                    response.Message += "Email boş bırakılmamalı.";
                }

                if (!user.email.IsValidEmail())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("email", "Geçerli bir email adresi giriniz."));
                    response.Message += "Geçerli bir email adresi giriniz.";
                }

                if (user.password.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("password", "Şifre boş bırakılmamalı."));
                    response.Message += "Şifre boş bırakılmamalı.";
                }

                if (user.password.IsNotNullOrEmpty() && user.password.Length != 8)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("password", "Şifre 8 haneli olmalıdır."));
                    response.Message += "Şifre 8 haneli olmalıdır.";
                }

                if (user.fullName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("fullName", "İsim boş bırakılmamalı."));
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _userService.SaveUserAsync(user);

                await _mailHandler.SendEmailAsync(
                    new MailRequest()
                    {
                        ToEmail = user.email,
                        Subject = "RandevuAPP' e Hoşgeldiniz",
                        Body = "<h1>RANDEVU APP</h1>" +
                                "<p>Hesabınızın onaylanması için " +
                                "<a href=\"" + getConfirmUrl(user.password + "." + user.id) + "\">buraya</a> tıklayınız." +
                                "</p>"
                    }
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kayıt yapılırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        private string getConfirmUrl(string key)
        {
            var url = $"{Request.Scheme}://{Request.Host}/";
            url += "user/confirm?key=" + key.Replace("=", "~");

            return url;
        }
    }
}
