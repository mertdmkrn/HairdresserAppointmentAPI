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
    public class BusinessCategoryController : ControllerBase
    {
        private IBusinessCategoryService _businessCategoryService;

        public BusinessCategoryController(IBusinessCategoryService businessCategoryService)
        {
            _businessCategoryService = businessCategoryService;
        }

        /// <summary>
        /// Get BusinessCategory By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businesscategory")]
        public async Task<IActionResult> GetBusinessCategoryById(int id)
        {
            ResponseModel<BusinessCategory> response = new ResponseModel<BusinessCategory>();

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

                response.Data = await _businessCategoryService.GetBusinessCategoryByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işyeri kategori kaydı bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri kategori kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get BusinessCategory By BusinessId Or CategoryId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businesscategories")]
        public async Task<IActionResult> GetBusinessCategoryByBusinessId(int? businessId, int? categoryId)
        {
            ResponseModel<IList<BusinessCategory>> response = new ResponseModel<IList<BusinessCategory>>();

            try
            {
                if (!(businessId.HasValue && businessId.Value > 0 || categoryId.HasValue && categoryId.Value > 0))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("parameter", "BusinessId veya CategoryId parametresinde biri 0' dan büyük olmalı."));
                    response.Message += "BusinessId veya CategoryId parametresinde biri 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);


                if (businessId.HasValue)
                    response.Data = await _businessCategoryService.GetBusinessCategoryByBusinessIdAsync(businessId.Value);
                else if(categoryId.HasValue)
                    response.Data = await _businessCategoryService.GetBusinessCategoryByCategoryIdAsync(categoryId.Value);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri kategori kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Create New BusinessCategory
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "categoryId": 1,
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("businesscategory/save")]
        public async Task<IActionResult> Save([FromBody] BusinessCategory businessCategory)
        {
            ResponseModel<BusinessCategory> response = new ResponseModel<BusinessCategory>();

            try
            {
                if (businessCategory.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId parametresi 0' dan büyük olmalı."));
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (businessCategory.categoryId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("categoryId", "CategoryId parametresi 0' dan büyük olmalı."));
                    response.Message += "CategoryId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessCategoryService.SaveBusinessCategoryAsync(businessCategory);

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
        /// Delete BusinessCategory
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("businesscategory/delete")]
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

                BusinessCategory businessCategory = await _businessCategoryService.GetBusinessCategoryByIdAsync(id);

                if (businessCategory == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işyeri kategori kaydı bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _businessCategoryService.DeleteBusinessCategoryAsync(businessCategory);

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
        /// Delete BusinessCategory By BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("businesscategories/delete")]
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

                var businessCategories = await _businessCategoryService.GetBusinessCategoryByBusinessIdAsync(businessId);

                if (businessCategories.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.Message += businessId + " businessId' li işyeri kategori kayıtları bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _businessCategoryService.DeleteBusinessCategoriesAsync(businessCategories);

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
