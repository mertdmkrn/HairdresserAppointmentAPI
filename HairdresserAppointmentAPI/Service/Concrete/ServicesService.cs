﻿using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class ServicesService : IServicesService
    {
        private IServicesRepository _servicesRepository;

        public ServicesService()
        {
            _servicesRepository = new ServicesRepository();
        }

        public async Task<Services> GetServicesByIdAsync(int id)
        {
            return await _servicesRepository.GetServicesByIdAsync(id);
        }

        public async Task<IList<Services>> GetServicesByBusinessIdAsync(int businessId)
        {
            return await _servicesRepository.GetServicesByBusinessIdAsync(businessId);
        }

        public async Task<Services> SaveServicesAsync(Services services)
        {
            return await _servicesRepository.SaveServicesAsync(services);
        }

        public async Task<IList<Services>> SaveServicesListAsync(List<Services> servicesList, int businessId)
        {
            return await _servicesRepository.SaveServicesListAsync(servicesList, businessId);
        }

        public async Task<Services> UpdateServicesAsync(Services services)
        {
            return await _servicesRepository.UpdateServicesAsync(services);
        }

        public async Task<bool> DeleteServicesAsync(Services services)
        {
            return await _servicesRepository.DeleteServicesAsync(services);
        }

        public async Task<bool> DeleteServicesListAsync(List<Services> servicesList)
        {
            return await _servicesRepository.DeleteServicesListAsync(servicesList);
        }
    }
}