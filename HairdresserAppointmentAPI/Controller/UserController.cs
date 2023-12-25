using HairdresserAppointmentAPI.Handler.Abstract;
using HairdresserAppointmentAPI.Handler.Concrete;
using HairdresserAppointmentAPI.Handler.Model;
using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ITokenHandler _tokenHandler;

        public UserController(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
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
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametresi 0' dan büyük olmalı."));
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _userService.GetUserById(id);

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
        /// Update User
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
                if (updateUser.fullName.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("fullName", "İsim boş bırakılmamalı."));
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                User user = await _userService.GetUserById(updateUser.id);

                if (user == null)
                {
                    response.HasError = true;
                    response.Message += updateUser.id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                user.imagePath = updateUser.imagePath;
                user.fullName = updateUser.fullName;

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
        [Route("user/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (id == 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametresi 0' dan büyük olmalı."));
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                User user = await _userService.GetUserById(id);

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
