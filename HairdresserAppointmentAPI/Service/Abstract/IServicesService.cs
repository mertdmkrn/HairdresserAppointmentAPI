using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IServicesService
    {
        Task<Services> GetServicesByIdAsync(int id);
        Task<IList<Services>> GetServicesByBusinessIdAsync(int businessId);
        Task<Services> SaveServicesAsync(Services services);
        Task<IList<Services>> SaveServicesListAsync(List<Services> servicesList, int businessId);
        Task<Services> UpdateServicesAsync(Services services);
        Task<bool> DeleteServicesAsync(Services services);
        Task<bool> DeleteServicesListAsync(IList<Services> servicesList);
    }
}
