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
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Get Rating By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("rating")]
        public async Task<IActionResult> GetRatingById(int id)
        {
            ResponseModel<Rating> response = new ResponseModel<Rating>();

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

                response.Data = await _ratingService.GetRatingByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li değerlendirme bulunamadı.";
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
        /// Get Rating By UserId And BusinessId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ratings")]
        public async Task<IActionResult> GetRatingByUserIdWithBusinessId(int? userId, int? businessId)
        {
            ResponseModel<IList<Rating>> response = new ResponseModel<IList<Rating>>();

            try
            {
                if (!((userId.HasValue && userId.Value > 0) || (businessId.HasValue && businessId.Value > 0)))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametrelerinini biri 0' dan büyük olmalı."));
                    response.Message += "Id parametrelerinini biri 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                if (userId.HasValue && userId.Value > 0 && !businessId.HasValue)
                    response.Data = await _ratingService.GetRatingsByUserIdAsync(userId.Value);
                else if (businessId.HasValue && businessId.Value > 0 && !userId.HasValue)
                    response.Data = await _ratingService.GetRatingsByBusinessIdAsync(businessId.Value);
                else
                    response.Data = await _ratingService.GetRatingsByUserIdWithBusinessIdAsync(userId.Value, businessId.Value);


                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += "Değerlendirme bulunamadı.";
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
        /// Create New Rating
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "comment": "Çok nezih bir kuaför",
        ///        "point": 4.5,
        ///        "userId": 1,
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("rating/save")]
        public async Task<IActionResult> Save([FromBody] Rating rating)
        {
            ResponseModel<Rating> response = new ResponseModel<Rating>();
            try
            {
                if (rating.comment.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("comment", "Yorum alanı boş bırakılmamalı."));
                    response.Message += "Yorum alanı boş bırakılmamalı.";
                }

                if (rating.point <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("point", "Puan alanı sıfırdan büyük olmalı."));
                    response.Message += "Geçerli bir email adresi giriniz.";
                }

                if (rating.userId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("userId", "UserId 0' dan büyük olmalı."));
                    response.Message += "UserId 0' dan büyük olmalı.";
                }

                if (rating.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("businessId", "BusinessId 0' dan büyük olmalı."));
                    response.Message += "BusinessId 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _ratingService.SaveRatingAsync(rating);
                
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
        /// Update Rating
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "comment": "Oldukça nezih bir kuaför.",
        ///        "point": 4.7
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("rating/update")]
        public async Task<IActionResult> Update([FromBody] Rating updateRating)
        {
            ResponseModel<Rating> response = new ResponseModel<Rating>();
            try
            {
                if (updateRating.comment.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("comment", "Yorum alanı boş bırakılmamalı."));
                    response.Message += "Yorum alanı boş bırakılmamalı.";
                }

                if (updateRating.point <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("point", "Puan alanı sıfırdan büyük olmalı."));
                    response.Message += "Geçerli bir email adresi giriniz.";
                }


                if (response.HasError)
                    return BadRequest(response);

                Rating rating = await _ratingService.GetRatingByIdAsync(updateRating.id);

                if (rating == null)
                {
                    response.HasError = true;
                    response.Message += updateRating.id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                rating.comment = updateRating.comment.IsNull(rating.comment);
                rating.point = updateRating.point;

                response.Data = await _ratingService.UpdateRatingAsync(rating);

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
        /// Delete Rating
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("rating/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if (id == 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add(new ValidationError("id", "Id parametresi 0' dan büyük olmalı.")  );
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                Rating rating = await _ratingService.GetRatingByIdAsync(id);

                if (rating == null) {
                    response.HasError = true;
                    response.Message += id + " id' li değerlendirme bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _ratingService.DeleteRatingAsync(rating);

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
