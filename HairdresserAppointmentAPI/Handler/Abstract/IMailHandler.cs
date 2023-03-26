using HairdresserAppointmentAPI.Handler.Model;
using System.Security.Claims;

namespace HairdresserAppointmentAPI.Handler.Abstract
{
    public interface IMailHandler
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
