using HairdresserAppointmentAPI.Model;
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

        public async Task<IList<Business>> GetBusinessByCountryAsync(string country, int page = 0, int take = 10)
        {
            return await _businessRepository.GetBusinessByCountryAsync(country, page, take);
        }

        public async Task<Business> GetBusinessByEmailAndPasswordAsync(string email, string password)
        {
            return await _businessRepository.GetBusinessByEmailAndPasswordAsync(email, password); 
        }

        public async Task<IList<Business>> GetBusinessByCountryAndProvinceAsync(string country, string province, int page = 0, int take = 10)
        {
            return await _businessRepository.GetBusinessByCountryAndProvinceAsync(country, province, page, take);
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            return await _businessRepository.GetBusinessByIdAsync(id);
        }

        public async Task<IList<Business>> GetBusinessNearByDistanceAsync(double latitude, double longitude, int metre)
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
