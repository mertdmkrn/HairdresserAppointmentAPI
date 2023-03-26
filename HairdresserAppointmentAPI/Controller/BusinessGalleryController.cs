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
    public class BusinessGalleryController : ControllerBase
    {
        private IBusinessGalleryService _businessGalleryService;

        public BusinessGalleryController()
        {
            _businessGalleryService = new BusinessGalleryService();
        }

        /// <summary>
        /// Get BusinessGallery By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessgallery")]
        public async Task<IActionResult> GetBusinessGalleryById(int id)
        {
            ResponseModel<BusinessGallery> response = new ResponseModel<BusinessGallery>();

            try
            {
                if (id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessGalleryService.GetBusinessGalleryByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işyeri resim kaydı bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri resim kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get BusinessGallery By BusinessId And Size
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessgalleries")]
        public async Task<IActionResult> GetBusinessGalleryByBusinessId(int businessId, string size)
        {
            ResponseModel<IList<BusinessGallery>> response = new ResponseModel<IList<BusinessGallery>>();

            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                if (size.IsNullOrEmpty())
                    response.Data = await _businessGalleryService.GetBusinessGalleryByBusinessIdAsync(businessId);
                else
                    response.Data = await _businessGalleryService.GetBusinessGalleryByBusinessIdAndSizeAsync(businessId, size);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri resim kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Create New BusinessGallery
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "imagePath": "https://randevufiles/guzellik.png",
        ///        "size": "300x250",
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("businessgallery/save")]
        public async Task<IActionResult> Save([FromBody] BusinessGallery businessGallery)
        {
            ResponseModel<BusinessGallery> response = new ResponseModel<BusinessGallery>();

            try
            {
                if (businessGallery.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (businessGallery.imagePath.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("imagePath", "Resim yolu dolu olmalıdır.");
                    response.Message += "Resim yolu dolu olmalıdır.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessGalleryService.SaveBusinessGalleryAsync(businessGallery);

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
        /// Create New BusinessGalleries
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        /// [
        ///     { 
        ///        "imagePath": "https://randevufiles/guzellik1.png",
        ///        "size": "300x250"
        ///     },
        ///     { 
        ///        "imagePath": "https://randevufiles/guzellik2.png",
        ///        "size": "960x720"
        ///     },
        ///     { 
        ///        "imagePath": "https://randevufiles/guzellik3.png",
        ///        "size": "1200x720"
        ///     }
        /// ]
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("businessgallery/savelist")]
        public async Task<IActionResult> SaveList([FromBody] List<BusinessGallery> businessGalleries, int businessId)
        {
            ResponseModel<IList<BusinessGallery>> response = new ResponseModel<IList<BusinessGallery>>();

            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessGalleryService.SaveBusinessGalleriesAsync(businessGalleries, businessId);

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
        /// Update BusinessGallery
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "imagePath": "https://randevufiles/guzellik4.png",
        ///        "size": "1200x900"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("businessgallery/update")]
        public async Task<IActionResult> Update([FromBody] BusinessGallery updateBusinessGallery)
        {
            ResponseModel<BusinessGallery> response = new ResponseModel<BusinessGallery>();
            try
            {
                if (updateBusinessGallery.id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                BusinessGallery businessGallery = await _businessGalleryService.GetBusinessGalleryByIdAsync(updateBusinessGallery.id);

                if (businessGallery == null)
                {
                    response.HasError = true;
                    response.Message += updateBusinessGallery.id + " id' li resim bulunamadı.";
                    return NotFound(response);
                }

                businessGallery.imagePath = updateBusinessGallery.imagePath.IsNull(businessGallery.imagePath);
                businessGallery.size = updateBusinessGallery.size.IsNull(businessGallery.size);

                response.Data = await _businessGalleryService.UpdateBusinessGalleryAsync(businessGallery);

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
        /// Delete BusinessGallery
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("businessgallery/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                BusinessGallery businessGallery = await _businessGalleryService.GetBusinessGalleryByIdAsync(id);

                if (businessGallery == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işyeri resim kaydı bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _businessGalleryService.DeleteBusinessGalleryAsync(businessGallery);

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
        /// Delete BusinessGallery By BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("businessgalleries/delete")]
        public async Task<IActionResult> DeleteByBusinessId(int businessId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                var businessGalleries = await _businessGalleryService.GetBusinessGalleryByBusinessIdAsync(businessId);

                if (businessGalleries.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.Message += businessId + " businessId' li işyeri resim kayıtları bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _businessGalleryService.DeleteBusinessGalleriesAsync(businessGalleries);

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
