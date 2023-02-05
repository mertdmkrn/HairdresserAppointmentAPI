using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Repository.Concrete;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Service.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        
        public UserService() {
            _userRepository = new UserRepository();
        }

        public User UserGetUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.UserGetUserByEmailAndPassword(email, password);
        }
    }
}
