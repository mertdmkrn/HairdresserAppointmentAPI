using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Model.CustomModel;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password);
        Task<IList<BusinessListModel>> GetBusinessByCityAsync(string city, double? latitude, double? longitude, int? page, int? take);
        Task<IList<BusinessListModel>> GetBusinessByCityAndProvinceAsync(string city, string province, double? latitude, double? longitude, int? page, int? take);
        Task<IList<BusinessListModel>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre);
        Task<Business> SaveBusinessAsync(Business business);
        Task<Business> UpdateBusinessAsync(Business business);
        Task<bool> DeleteBusinessAsync(Business business);
    }
}
