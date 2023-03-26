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
    public class BusinessWorkingInfoController : ControllerBase
    {
        private IBusinessWorkingInfoService _businessWorkingInfoService;

        public BusinessWorkingInfoController()
        {
            _businessWorkingInfoService = new BusinessWorkingInfoService();
        }

        /// <summary>
        /// Get BusinessWorkingInfo By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessworkinginfo")]
        public async Task<IActionResult> GetBusinessWorkingInfoById(int id)
        {
            ResponseModel<BusinessWorkingInfo> response = new ResponseModel<BusinessWorkingInfo>();

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

                response.Data = await _businessWorkingInfoService.GetBusinessWorkingInfoByIdAsync(id);

                if (response.Data == null)
                {
                    response.HasError = true;
                    response.Message += id + " id' li işyeri çalışma bilgisi kaydı bulunamadı.";
                    return NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri çalışma bilgisi kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Get BusinessWorkingInfo By BusinessId, StartDate, End Date
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessworkinginfos")]
        public async Task<IActionResult> GetBusinessWorkingInfoByBusinessId(int? businessId, DateTime? startDate, DateTime? endDate)
        {
            ResponseModel<IList<BusinessWorkingInfo>> response = new ResponseModel<IList<BusinessWorkingInfo>>();

            try
            {
                if (!(businessId.HasValue && businessId.Value > 0))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresine 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresine 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);


                if (startDate.HasValue && endDate.HasValue)
                    response.Data = await _businessWorkingInfoService.GetBusinessWorkingInfoByBusinessIdAndBetweenDateAsync(businessId.Value, startDate.Value, endDate.Value);
                else if(startDate.HasValue)
                    response.Data = await _businessWorkingInfoService.GetBusinessWorkingInfoByBusinessIdAndDateAsync(businessId.Value, startDate.Value);
                else
                    response.Data = await _businessWorkingInfoService.GetBusinessWorkingInfoByBusinessIdAsync(businessId.Value);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message += "İşyeri çalışma bilgisi kaydı aranırken hata. Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Create New BusinessWorkingInfo
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "date": "2023-03-21",
        ///        "startHour": "08:30",
        ///        "endHour": "21:30",
        ///        "appointmentTimeInterval": 60,
        ///        "appointmentPeopleCount": 10,
        ///        "businessId": 1
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("businessworkinginfo/save")]
        public async Task<IActionResult> Save([FromBody] BusinessWorkingInfo businessWorkingInfo)
        {
            ResponseModel<BusinessWorkingInfo> response = new ResponseModel<BusinessWorkingInfo>();

            try
            {
                if (businessWorkingInfo.businessId <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessWorkingInfoService.SaveBusinessWorkingInfoAsync(businessWorkingInfo);

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
        /// Create New BusinessWorkingInfo List
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///     
        ///     [
        ///         { 
        ///             "date": "2023-03-21",
        ///             "startHour": "08:30",
        ///             "endHour": "21:30",
        ///             "appointmentTimeInterval": 60,
        ///             "appointmentPeopleCount": 10
        ///         },
        ///         { 
        ///             "date": "2023-03-22",
        ///             "startHour": "08:30",
        ///             "endHour": "21:30",
        ///             "appointmentTimeInterval": 60,
        ///             "appointmentPeopleCount": 10
        ///         },
        ///         { 
        ///             "date": "2023-03-22",
        ///             "startHour": "08:30",
        ///             "endHour": "21:30",
        ///             "appointmentTimeInterval": 60,
        ///             "appointmentPeopleCount": 10
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("businessworkinginfos/save")]
        public async Task<IActionResult> SaveByBusinessId([FromBody] List<BusinessWorkingInfo> businessWorkingInfos, int? businessId)
        {
            ResponseModel<IList<BusinessWorkingInfo>> response = new ResponseModel<IList<BusinessWorkingInfo>>();

            try
            {
                if (!(businessId.HasValue && businessId.Value > 0))
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("businessId", "BusinessId parametresi 0' dan büyük olmalı.");
                    response.Message += "BusinessId parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                response.Data = await _businessWorkingInfoService.SaveBusinessWorkingInfosAsync(businessWorkingInfos, businessId.Value);

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
        /// Update BusinessWorkingInfo
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     {
        ///        "id": 1,
        ///        "date": "2023-03-21",
        ///        "startHour": "08:30",
        ///        "endHour": "21:30",
        ///        "appointmentTimeInterval": 60,
        ///        "appointmentPeopleCount": 10,
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("businessworkinginfo/update")]
        public async Task<IActionResult> Update([FromBody] BusinessWorkingInfo updateBusinessWorkingInfo)
        {
            ResponseModel<BusinessWorkingInfo> response = new ResponseModel<BusinessWorkingInfo>();
            try
            {
                if (updateBusinessWorkingInfo.id <= 0)
                {
                    response.HasError = true;
                    response.ValidationErrors.Add("id", "Id parametresi 0' dan büyük olmalı.");
                    response.Message += "Id parametresi 0' dan büyük olmalı.";
                }

                if (response.HasError)
                    return BadRequest(response);

                BusinessWorkingInfo businessWorkingInfo = await _businessWorkingInfoService.GetBusinessWorkingInfoByIdAsync(updateBusinessWorkingInfo.id);

                if (businessWorkingInfo == null)
                {
                    response.HasError = true;
                    response.Message += updateBusinessWorkingInfo.id + " id' li işyeri çalışma bilgisi kaydı bulunamadı.";
                    return NotFound(response);
                }

                businessWorkingInfo.startHour = updateBusinessWorkingInfo.startHour.IsNull(businessWorkingInfo.startHour);
                businessWorkingInfo.endHour = updateBusinessWorkingInfo.endHour.IsNull(businessWorkingInfo.endHour);
                businessWorkingInfo.appointmentPeopleCount = updateBusinessWorkingInfo.appointmentPeopleCount > 0 ? updateBusinessWorkingInfo.appointmentPeopleCount : businessWorkingInfo.appointmentPeopleCount;
                businessWorkingInfo.appointmentTimeInterval = updateBusinessWorkingInfo.appointmentTimeInterval > 0 ? updateBusinessWorkingInfo.appointmentTimeInterval : businessWorkingInfo.appointmentTimeInterval;

                response.Data = await _businessWorkingInfoService.UpdateBusinessWorkingInfoAsync(businessWorkingInfo);

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
