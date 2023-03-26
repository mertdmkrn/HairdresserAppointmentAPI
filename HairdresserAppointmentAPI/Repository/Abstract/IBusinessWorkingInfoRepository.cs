using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IBusinessWorkingInfoRepository
    {
        Task<BusinessWorkingInfo> GetBusinessWorkingInfoByIdAsync(long id);
        Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAsync(int businessId);
        Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndDateAsync(int businessId, DateTime? date);
        Task<IList<BusinessWorkingInfo>> GetBusinessWorkingInfoByBusinessIdAndBetweenDateAsync(int businessId, DateTime? startDate, DateTime? endDate);
        Task<BusinessWorkingInfo> SaveBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo);
        Task<IList<BusinessWorkingInfo>> SaveBusinessWorkingInfosAsync(List<BusinessWorkingInfo> businessWorkingInfos, int businessId);
        Task<BusinessWorkingInfo> UpdateBusinessWorkingInfoAsync(BusinessWorkingInfo businessWorkingInfo);
    }
}
