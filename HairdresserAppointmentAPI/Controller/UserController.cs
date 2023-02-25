using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            ResponseModel<IList<User>> response = new ResponseModel<IList<User>>();
            response.Data = await _userService.GetUsersAsync();

            return Ok(response);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            ResponseModel<User> response = new ResponseModel<User>();

            try
            {
                if (id == 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = _userService.GetUserById(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kullanıcı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }

        }

        /// <summary>
        /// User Login Control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string? email, string? password)
        {
            ResponseModel<User> response = new ResponseModel<User>();

            if (email.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add("email", "Email boş bırakılmamalı.");
                response.Message += "Email boş bırakılmamalı.";
            }

            if (password.IsNullOrEmpty())
            {
                response.HasError = true;
                response.ValidationErrors.Add("password", "Şifre boş bırakılmamalı.");
                response.Message += "Şifre boş bırakılmamalı.";
            }

            if (response.HasError)
                return BadRequest(response);

            response.Data = await _userService.GetUserByEmailAndPasswordAsync(email, password);

            if (response.Data == null)
            {
                response.HasError = true;
                response.Message = "Girdiğiniz bilgilere ait kullanıcı bulunamadı.";
                return NotFound(response);
            }

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
                    response.ValidationErrors.Add("email", "Email boş bırakılmamalı.");
                    response.Message += "Email boş bırakılmamalı.";
                }

                if (!user.email.IsValidEmail())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("email", "Geçerli bir email adresi giriniz.");
                    response.Message += "Geçerli bir email adresi giriniz.";
                }

                if (user.password.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("password", "Şifre boş bırakılmamalı.");
                    response.Message += "Şifre boş bırakılmamalı.";
                }

                if (user.password.IsNotNullOrEmpty() && user.password.Length != 8)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("password", "Şifre 8 haneli olmalıdır.");
                    response.Message += "Şifre 8 haneli olmalıdır.";
                }

                if (user.firstName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("firstname", "İsim boş bırakılmamalı.");
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (user.lastName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("lastname", "Soyisim boş bırakılmamalı.");
                    response.Message += "Soyisim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _userService.SaveUserAsync(user);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kayıt yapılırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "firstName": "Mert",
        ///        "lastName": "DEMİRKIRAN",
        ///        "imagePath": "images/mertdemirkiran.jpg"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("user/update")]
        public async Task<IActionResult> Update([FromBody] User updateUser)
        {
            ResponseModel<User> response = new ResponseModel<User>();
            try
            {
                if (updateUser.firstName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("firstname", "İsim boş bırakılmamalı.");
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (updateUser.lastName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("lastname", "Soyisim boş bırakılmamalı.");
                    response.Message += "Soyisim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                User user = _userService.GetUserById(updateUser.id);

                if (user == null)
                {
                    response.HasError = true;
                    response.Message += updateUser.id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                user.imagePath = updateUser.imagePath;
                user.firstName = updateUser.firstName;
                user.lastName = updateUser.lastName;

                response.Data = await _userService.UpdateUserAsync(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kayıt yapılırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("user/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (id == 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                User user = _userService.GetUserById(id);

                if (user == null) {
                    response.HasError = true;
                    response.Message += id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _userService.DeleteUserAsync(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kayıt yapılırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }
    }
}
