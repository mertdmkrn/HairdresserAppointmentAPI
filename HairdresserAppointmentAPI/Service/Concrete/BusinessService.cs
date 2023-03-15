using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Model.CustomModel;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class BusinessService : IBusinessService
    {
        private IBusinessRepository _businessRepository;

        public BusinessService()
        {
            _businessRepository = new BusinessRepository();
        }

        public async Task<IList<BusinessListModel>> GetBusinessByCityAsync(string city, double? latitude, double? longitude, int? page, int? take)
        {
            return await _businessRepository.GetBusinessByCityAsync(city, latitude, longitude, page, take);
        }

        public async Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password)
        {
            return await _businessRepository.GetBusinessByEmailAndPasswordAsync(email, password); 
        }

        public async Task<IList<BusinessListModel>> GetBusinessByCityAndProvinceAsync(string city, string province, double? latitude, double? longitude, int? page, int? take)
        {
            return await _businessRepository.GetBusinessByCityAndProvinceAsync(city, province, latitude, longitude, page, take);
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            return await _businessRepository.GetBusinessByIdAsync(id);
        }

        public async Task<IList<BusinessListModel>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre)
        {
            return await _businessRepository.GetBusinessNearByDistanceAsync(latitude, longitude, metre); 
        }

        public async Task<Business> SaveBusinessAsync(Business business)
        {
            return await _businessRepository.SaveBusinessAsync(business);
        }

        public async Task<Business> UpdateBusinessAsync(Business business)
        {
            return await _businessRepository.UpdateBusinessAsync(business);
        }

        public async Task<bool> DeleteBusinessAsync(Business business)
        {
            return await _businessRepository.DeleteBusinessAsync(business);
        }
    }
}
