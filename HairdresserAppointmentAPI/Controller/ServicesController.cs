using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        private IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        /// <summary>
        /// Get Services By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("service")]
        public async Task<IActionResult> GetServicesById(int id)
        {
            ResponseModel<Services> response = new ResponseModel<Services>();

            try
            {
                if (id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametresi 0' dan büyük olmalı."));
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _servicesService.GetServicesByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li hizmet bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Hizmet aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get Services By BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("services")]
        public async Task<IActionResult> GetServicesByBusinessId(int businessId)
        {
            ResponseModel<IList<Services>> response = new ResponseModel<IList<Services>>();

            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId parametresi 0' dan büyük olmalı."));
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                response.Data = await _servicesService.GetServicesByBusinessIdAsync(businessId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Hizmet aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Create New Service
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "name": "Ombre",
        ///        "price": 100,
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("service/save")]
        public async Task<IActionResult> Save([FromBody] Services services)
        {
            ResponseModel<Services> response = new ResponseModel<Services>();

            try
            {
                if (services.name.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("name", "Hizmet isim alanı boş bırakılmamalı."));
                    response.Message += "Hizmet isim alanı boş bırakılmamalı.";
                }

                if (services.price < 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("price", "Fiyat alanı 0' dan küçük olmamalıdır."));
                    response.Message += "Fiyat alanı 0' dan küçük olmamalıdır.";
                }

                if (services.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId parametresi 0' dan büyük olmalı."));
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _servicesService.SaveServicesAsync(services);

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
        /// Create New Services
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        /// [
        ///     { 
        ///         "name": "Saç Kesim",
        ///         "price": 100
        ///     },
        ///     { 
        ///         "name": "Saç Boyama",
        ///         "price": 150
        ///     },
        ///     { 
        ///         "name": "Fön",
        ///         "price": 50
        ///     }
        /// ]
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("service/savelist")]
        public async Task<IActionResult> SaveList([FromBody] List<Services> services, int businessId)
        {
            ResponseModel<IList<Services>> response = new ResponseModel<IList<Services>>();

            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId parametresi 0' dan büyük olmalı."));
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (services.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("services", "Hizmet listesi boş olmalalıdır."));
                    response.Message += "Hizmet listesi boş olmalalıdır.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _servicesService.SaveServicesListAsync(services, businessId);

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
        /// Update Service
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "name": "Saç Kesimi",
        ///        "price": "120"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("service/update")]
        public async Task<IActionResult> Update([FromBody] Services updateServices)
        {
            ResponseModel<Services> response = new ResponseModel<Services>();
            try
            {

                if (updateServices.id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("Id", "Id parametresi 0' dan büyük olmalı."));
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                Services services = await _servicesService.GetServicesByIdAsync(updateServices.id);

                if (services == null)
                {
                    response.HasError = true;
                    response.Message += updateServices.id + " id' li hizmet bulunamadı.";
                    return NotFound(response);
                }

                services.name = updateServices.name.IsNull(services.name);
                services.price = updateServices.price >= 0 ? updateServices.price : services.price;

                response.Data = await _servicesService.UpdateServicesAsync(services);

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
        /// Delete Service
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("service/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametresi 0' dan büyük olmalı."));
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                Services services = await _servicesService.GetServicesByIdAsync(id);

                if (services == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li hizmet bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _servicesService.DeleteServicesAsync(services);

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
        /// Delete Service By BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("services/delete")]
        public async Task<IActionResult> DeleteByBusinessId(int businessId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId parametresi 0' dan büyük olmalı."));
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                var serviceList = await _servicesService.GetServicesByBusinessIdAsync(businessId);

                if (serviceList.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.Message += businessId + " businessId' li hizmetler bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _servicesService.DeleteServicesListAsync(serviceList);

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
