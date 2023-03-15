using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class BusinessWorkingInfoService : IBusinessWorkingInfoService
    {
        private IBusinessWorkingInfoRepository _businessWorkingInfoRepository;

        public BusinessWorkingInfoService()
        {
            _businessWorkingInfoRepository = new BusinessWorkingInfoRepository();
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndBetweenDateAsync(int businessId, DateTime? startDate, DateTime? endDate)
        {
            return await _businessWorkingInfoRepository.GetBusinessWorkingInfoByBusinessIdAndBetweenDateAsync(businessId, startDate, endDate);
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndDateAsync(int businessId, DateTime? date)
        {
            return await _businessWorkingInfoRepository.GetBusinessWorkingInfoByBusinessIdAndDateAsync(businessId, date);
        }

        public async Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAsync(int businessId)
        {
            return await _businessWorkingInfoRepository.GetBusinessWorkingInfoByBusinessIdAsync(businessId);
        }

        public async Task<BusinessWorkingInfo> GetBusinessWorkingInfoByIdAsync(int id)
        {
            return await _businessWorkingInfoRepository.GetBusinessWorkingInfoByIdAsync(id);
        }

        public async Task<BusinessWorkingInfo> SaveBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo)
        {
            return await _businessWorkingInfoRepository.SaveBusinessWorkingInfoAsync(businessWorkingInfo);
        }

        public async Task<IList<BusinessWorkingInfo>> SaveBusinessWorkingInfosAsync(List<BusinessWorkingInfo> businessWorkingInfos, int businessId)
        {
            return await _businessWorkingInfoRepository.SaveBusinessWorkingInfosAsync(businessWorkingInfos, businessId);
        }

        public async Task<BusinessWorkingInfo> UpdateBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo)
        {
            return await _businessWorkingInfoRepository.UpdateBusinessWorkingInfoAsync(businessWorkingInfo);
        }
    }
}
