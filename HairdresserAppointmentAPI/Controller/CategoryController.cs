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
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        /// <summary>
        /// Get Category By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetCategoryByIdOrName(int? id, string? name)
        {
            ResponseModel<Category> response = new ResponseModel<Category>();

            try
            {
                if ((!id.HasValue || id.Value <= 0) && (name.IsNullOrEmpty()))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("parameter", "Id veya name parametresinden biri olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                if(id.HasValue)
                    response.Data = await _categoryService.GetCategoryByIdAsync(id.Value);
                else if(name.IsNotNullOrEmpty())
                    response.Data = await _categoryService.GetCategoryByNameAsync(name);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += (id.HasValue ? "Id : " + id.Value : string.Empty) + 
                                        (name.IsNotNullOrEmpty() ? " Name : " + name : string.Empty) +
                                        " parametrelerine sahip kategori bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "Kategori aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            ResponseModel<IList<Category>> response = new ResponseModel<IList<Category>>();

            try
            {   
                response.Data = await _categoryService.GetCategoriesAsync();
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
        /// Create New Category
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "name": "Güzellik Salonu"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("category/save")]
        public async Task<IActionResult> Save([FromBody] Category category)
        {
            ResponseModel<Category> response = new ResponseModel<Category>();

            try
            {
                if (category.name.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("name", "Kategori isim alanı boş bırakılmamalı.");
                    response.Message += "Kategori isim alanı boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _categoryService.SaveCategoryAsync(category);
                
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
        /// Update Category
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "name": "Güzellik ve Bakım Salonu"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("category/update")]
        public async Task<IActionResult> Update([FromBody] Category updateCategory)
        {
            ResponseModel<Category> response = new ResponseModel<Category>();
            try
            {
                if (updateCategory.name.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("name", "Kategori isim alanı boş bırakılmamalı.");
                    response.Message += "Kategori isim alanı boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                Category category = await _categoryService.GetCategoryByIdAsync(updateCategory.id);

                if (category == null)
                {
                    response.HasError = true;
                    response.Message += updateCategory.id + " id' li kategori bulunamadı.";
                    return NotFound(response);
                }

                category.name = updateCategory.name.IsNull(category.name);

                response.Data = await _categoryService.UpdateCategoryAsync(category);

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
        /// Delete Category
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("category/delete")]
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

                Category category = await _categoryService.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li kategori bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _categoryService.DeleteCategoryAsync(category);

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
