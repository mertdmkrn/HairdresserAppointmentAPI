using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Service.Abstract
{
    public interface IUserService
    {
        public User UserGetUserByEmailAndPassword(string email, string password);
    }
}
