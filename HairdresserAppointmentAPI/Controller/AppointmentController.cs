using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentService _appointmentService;

        public AppointmentController()
        {
            _appointmentService = new AppointmentService();
        }

        /// <summary>
        /// Get Appointment By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("appointment")]
        public async Task<IActionResult> GetAppointmentById(long id)
        {
            ResponseModel<Appointment> response = new ResponseModel<Appointment>();

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

                response.Data = await _appointmentService.GetAppointmentByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li randevu bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşletme aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get Appointment By UserId And BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("appointments")]
        public async Task<IActionResult> GetAppointmentByUserIdWithBusinessId(int? userId, int? businessId, int? page, int? take)
        {
            ResponseModel<IList<Appointment>> response = new ResponseModel<IList<Appointment>>();

            try
            {
                if (!((userId.HasValue && userId.Value > 0) || (businessId.HasValue && businessId.Value > 0)))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametrelerinini biri 0' dan büyük olmalı.");
                    response.Message += "Id parametrelerinini biri 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                if (userId.HasValue && userId.Value > 0 && !businessId.HasValue)
                    response.Data = await _appointmentService.GetAppointmentsByUserIdAsync(userId.Value, page, take);
                else if (businessId.HasValue && businessId.Value > 0 && !userId.HasValue)
                    response.Data = await _appointmentService.GetAppointmentsByBusinessIdAsync(businessId.Value, page, take);
                else
                    response.Data = await _appointmentService.GetAppointmentsByUserIdWithBusinessIdAsync(userId.Value, businessId.Value, page, take);


                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += "Randevu bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşletme aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get Appointment By Date And UserId or BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("appointment/getbydate")]
        public async Task<IActionResult> GetAppointmentByUserIdWithBusinessId(DateTime? startDate, DateTime? endDate, int? userId, int? businessId, int? page, int? take)
        {
            ResponseModel<IList<Appointment>> response = new ResponseModel<IList<Appointment>>();

            try
            {
                if (!startDate.HasValue)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("startDate", "Başlangıç tarihi boş olmamalıdır.");
                    response.Message += "Başlangıç tarihi boş olmamalıdır.";
                }

                if (response.HasError)
                    return BadRequest(response);

                endDate = endDate.HasValue ? endDate : startDate;

                if (userId.HasValue && userId.Value > 0)
                    response.Data = await _appointmentService.GetAppointmentsByDateWithUserIdAsync(startDate, endDate, userId.Value, page, take);
                else if (businessId.HasValue && businessId.Value > 0)
                    response.Data = await _appointmentService.GetAppointmentsByDateWithBusinessIdAsync(startDate, endDate, businessId.Value, page, take);
                else
                    response.Data = await _appointmentService.GetAppointmentsByDateAsync(startDate, endDate, page, take);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += "Randevu bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşletme aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Create New Appointment
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "description": "Manikür ve pedikür yaptırıcam.",
        ///        "date": "2023-05-01T12:00:00",
        ///        "userId": 1,
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("appointment/save")]
        public async Task<IActionResult> Save([FromBody] Appointment appointment)
        {
            ResponseModel<Appointment> response = new ResponseModel<Appointment>();
            try
            {

                if (appointment.userId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("userId", "UserId 0' dan büyük olmalı.");
                    response.Message += "UserId 0' dan büyük olmalı.";
                }

                if (appointment.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId 0' dan büyük olmalı.");
                    response.Message += "BusinessId 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _appointmentService.SaveAppointmentAsync(appointment);
                
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
        /// Update Appointment
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "description": "Oldukça nezih bir kuaför.",
        ///        "status": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("appointment/update")]
        public async Task<IActionResult> Update([FromBody] Appointment updateAppointment)
        {
            ResponseModel<Appointment> response = new ResponseModel<Appointment>();
            try
            {
                if (response.HasError)
                    return BadRequest(response);

                Appointment appointment = await _appointmentService.GetAppointmentByIdAsync(updateAppointment.id);

                if (appointment == null)
                {
                    response.HasError = true;
                    response.Message += updateAppointment.id + " id' li randevu bulunamadı.";
                    return NotFound(response);
                }

                appointment.description = updateAppointment.description.IsNull(appointment.description);
                appointment.status = updateAppointment.status;

                response.Data = await _appointmentService.UpdateAppointmentAsync(appointment);

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
        /// Delete Appointment
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("appointment/delete/{id}")]
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

                Appointment Appointment = await _appointmentService.GetAppointmentByIdAsync(id);

                if (Appointment == null) {
                    response.HasError = true;
                    response.Message += id + " id' li randevu bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _appointmentService.DeleteAppointmentAsync(Appointment);

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
