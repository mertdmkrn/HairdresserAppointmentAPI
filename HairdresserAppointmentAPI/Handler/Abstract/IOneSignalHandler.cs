using HairdresserAppointmentAPI.Handler.Model;

namespace HairdresserAppointmentAPI.Handler.Abstract
{
    public interface IOneSignalHandler
    {
        Task<bool> CreateNotification(NotificationRequest notificationRequest);
    }
}
