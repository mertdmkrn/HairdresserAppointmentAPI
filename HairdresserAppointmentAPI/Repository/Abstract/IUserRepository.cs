using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IUserRepository
    {
        public User UserGetUserByEmailAndPassword(string email, string password);
    }
}
