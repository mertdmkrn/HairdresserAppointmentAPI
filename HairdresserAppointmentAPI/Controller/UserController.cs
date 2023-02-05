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

        public UserController() {
            _userService = new UserService();
        }

        /// <summary>
        /// User Login Control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string? email, string? password)
        {
            ResponseModel<User> response = new ResponseModel<User>();

            if (email.IsNullOrEmpty()) {
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

            response.Data = _userService.UserGetUserByEmailAndPassword(email, password);

            if (response.Data == null) {
                response.HasError = true;
                response.Message = "Girdiğiniz bilgilere ait kullanıcı bulunamadı.";
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
