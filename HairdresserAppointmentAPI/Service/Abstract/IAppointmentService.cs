using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IAppointmentService
    {
        Task<Appointment> GetAppointmentByIdAsync(long id);
        Task<IList<Appointment>> GetAppointmentsByBusinessIdAsync(int businessId, int? page, int? take);
        Task<IList<Appointment>> GetAppointmentsByUserIdAsync(int userId, int? page, int? take);
        Task<IList<Appointment>> GetAppointmentsByUserIdWithBusinessIdAsync(int userId, int businessId, int? page, int? take);
        Task<IList<Appointment>> GetAppointmentsByDateAsync(DateTime? startDate, DateTime? endDate, int? page, int? take);
        Task<IList<Appointment>> GetAppointmentsByDateWithUserIdAsync(DateTime? startDate, DateTime? endDate, int userId, int? page, int? take);
        Task<IList<Appointment>> GetAppointmentsByDateWithBusinessIdAsync(DateTime? startDate, DateTime? endDate, int businessId, int? page, int? take);
        Task<Appointment> SaveAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(Appointment appointment);
    }
}
