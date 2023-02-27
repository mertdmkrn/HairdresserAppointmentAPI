using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password);
        Task<IList<Business>> GetBusinessByCountryAsync(string country, int page = 0, int take = 10);
        Task<IList<Business>> GetBusinessByCountryAndProvinceAsync(string country, string province, int page = 0, int take = 10);
        Task<IList<Business>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre);
        Task<Business> SaveBusinessAsync(Business business);
        Task<Business> UpdateBusinessAsync(Business business);
        Task<bool> DeleteBusinessAsync(Business business);
    }
}
