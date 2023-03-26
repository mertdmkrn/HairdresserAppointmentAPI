using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Model.CustomModel;
using HairdresserAppointmentAPI.Model.SearchModel;
using HairdresserAppointmentAPI.Service.Abstract;
using HairdresserAppointmentAPI.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserAppointmentAPI.Controller
{
    [ApiController]
    [Authorize]
    public class BusinessController : ControllerBase
    {
        private IBusinessService _businessService;

        public BusinessController()
        {
            _businessService = new BusinessService();
        }

        /// <summary>
        /// Get Business By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("business/{id}")]
        public async Task<IActionResult> GetBusinessById(int id)
        {
            ResponseModel<Business> response = new ResponseModel<Business>();

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

                response.Data = await _businessService.GetBusinessByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işletme bulunamadı.";
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
        /// Get Business Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("business/search")]
        public async Task<IActionResult> GetBusinessBySearch([FromQuery] BusinessSearchModel businessSearchModel)
        {
            ResponseModel<IList<BusinessListModel>> response = new ResponseModel<IList<BusinessListModel>>();

            try
            {
                if (businessSearchModel.City.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("city", "City parametresi boş olmalı.");
                    response.Message += "City parametresi boş olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                if(!businessSearchModel.Province.IsNullOrEmpty())
                    response.Data = await _businessService.GetBusinessByCityAndProvinceAsync(businessSearchModel.City, businessSearchModel.Province, businessSearchModel.Latitude, businessSearchModel.Longitude, businessSearchModel.Page, businessSearchModel.Take);
                else
                    response.Data = await _businessService.GetBusinessByCityAsync(businessSearchModel.City, businessSearchModel.Latitude, businessSearchModel.Longitude, businessSearchModel.Page, businessSearchModel.Take);

                if (response.Data == null || response.Data.Count == 0)
                {
                    response.HasError = true;
                    response.Message += businessSearchModel.City + " şehrinde " + (!businessSearchModel.Province.IsNullOrEmpty() ? businessSearchModel.Province + " ilçesinde" : string.Empty) + " işletme bulunamadı.";
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
        /// Get Business User location Find Near By
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("business/findnearby")]
        public async Task<IActionResult> GetBusinessFindNearBy(double latitude, double longitude, int distance)
        {
            ResponseModel<IList<BusinessListModel>> response = new ResponseModel<IList<BusinessListModel>>();

            try
            {
                if (latitude <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("latitude", "Latitude parametresi 0' dan büyük olmalı.");
                    response.Message += "Latitude parametresi 0' dan büyük olmalı.";
                }

                if (longitude <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("longitude", "Longitude parametresi 0' dan büyük olmalı.");
                    response.Message += "Longitude parametresi 0' dan büyük olmalı.";
                }

                if (distance <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("distance", "Distance parametresi 0' dan büyük olmalı.");
                    response.Message += "Distance parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessService.GetBusinessNearByDistanceAsync(latitude, longitude, distance);

                if (response.Data == null || response.Data.Count == 0)
                {
                    response.HasError = true;
                    response.Message += "Konumunuza yakın " +  distance + " metre içinde işletme bulunamadı.";
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
        /// Create New Business
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "name": "Mert Güzellik Salonu",
        ///        "city": "İstanbul",
        ///        "province": "Kağıthane",
        ///        "district": "Çeliktepe",
        ///        "address": "Çeliktepe Mah. Polat Sk. No:16 D:8",
        ///        "telephone": "05467335939",
        ///        "email": "mertrandevuapp@gmail.com",
        ///        "password": "stms5581",
        ///        "latitude": 41.0821334,
        ///        "longitude": 28.9957109
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("business/save")]
        public async Task<IActionResult> Save([FromBody] Business business)
        {
            ResponseModel<Business> response = new ResponseModel<Business>();
            try
            {
                if (business.email.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("email", "Email boş bırakılmamalı.");
                    response.Message += "Email boş bırakılmamalı.";
                }

                if (!business.email.IsValidEmail())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("email", "Geçerli bir email adresi giriniz.");
                    response.Message += "Geçerli bir email adresi giriniz.";
                }

                if (business.password.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("password", "Şifre boş bırakılmamalı.");
                    response.Message += "Şifre boş bırakılmamalı.";
                }

                if (business.password.IsNotNullOrEmpty() && business.password.Length != 8)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("password", "Şifre 8 haneli olmalıdır.");
                    response.Message += "Şifre 8 haneli olmalıdır.";
                }

                if (business.name.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("name", "İsim boş bırakılmamalı.");
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessService.SaveBusinessAsync(business);
                
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
        /// Update User
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "id": 1,
        ///        "name": "Mert Güzellik Salonu",
        ///        "city": "İstanbul",
        ///        "province": "Kağıthane",
        ///        "district": "Çeliktepe",
        ///        "address": "Çeliktepe Mah. Polat Sk. No:16 D:8",
        ///        "telephone": "05467335939",
        ///        "latitude": 41.0821334,
        ///        "longitude": 28.9957109,
        ///        "imagePath": "images/mertguzelliksalonu.jpg"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("business/update")]
        public async Task<IActionResult> Update([FromBody] Business updateBusiness)
        {
            ResponseModel<Business> response = new ResponseModel<Business>();
            try
            {
                if (updateBusiness.name.IsNullOrEmpty())
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("name", "İsim boş bırakılmamalı.");
                    response.Message += "İsim boş bırakılmamalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                Business business = await _businessService.GetBusinessByIdAsync(updateBusiness.id);

                if (business == null)
                {
                    response.HasError = true;
                    response.Message += business.id + " id' li kullanıcı bulunamadı.";
                    return NotFound(response);
                }

                business.name = updateBusiness.name.IsNull(business.name);
                business.city = updateBusiness.city.IsNull(business.city);
                business.province = updateBusiness.province.IsNull(business.province);
                business.district = updateBusiness.district.IsNull(business.district);
                business.address = updateBusiness.address.IsNull(business.address);
                business.latitude = updateBusiness.latitude == 0 ? business.latitude : updateBusiness.latitude;
                business.longitude = updateBusiness.longitude == 0 ? business.longitude : updateBusiness.longitude;
                var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
                business.location = gf.CreatePoint(new NetTopologySuite.Geometries.Coordinate(business.latitude, business.longitude));

                response.Data = await _businessService.UpdateBusinessAsync(business);

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
        /// Delete Business
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("business/delete")]
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

                Business business = await _businessService.GetBusinessByIdAsync(id);

                if (business == null) {
                    response.HasError = true;
                    response.Message += id + " id' li işletme bulunamadı.";
                    return NotFound(response);
                }

                response.Data = await _businessService.DeleteBusinessAsync(business);

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
