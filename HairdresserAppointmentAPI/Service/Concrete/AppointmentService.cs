using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentRepository _appointmentRepository;
        
        public AppointmentService(IAppointmentRepository appointmentRepository) 
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(long id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<IList<Appointment>> GetAppointmentsByBusinessIdAsync(int businessId, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByBusinessIdAsync(businessId, page, take);
        }

        public async Task<IList<Appointment>> GetAppointmentsByUserIdAsync(int userId, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByUserIdAsync(userId, page, take);
        }

        public async Task<IList<Appointment>> GetAppointmentsByUserIdWithBusinessIdAsync(int userId, int businessId, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByUserIdWithBusinessIdAsync(userId, businessId, page, take);
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateAsync(DateTime? startDate, DateTime? endDate, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByDateAsync(startDate, endDate, page, take);
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateWithBusinessIdAsync(DateTime? startDate, DateTime? endDate, int businessId, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByDateWithBusinessIdAsync(startDate, endDate, businessId, page, take);
        }

        public async Task<IList<Appointment>> GetAppointmentsByDateWithUserIdAsync(DateTime? startDate, DateTime? endDate, int userId, int? page, int? take)
        {
            return await _appointmentRepository.GetAppointmentsByDateWithUserIdAsync(startDate, endDate, userId, page, take);
        }

        public async Task<Appointment> SaveAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.SaveAppointmentAsync(appointment);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.DeleteAppointmentAsync(appointment);
        }
    }
}
