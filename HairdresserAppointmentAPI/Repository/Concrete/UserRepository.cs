using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User UserGetUserByEmailAndPassword(string email, string password)
        {
            return _users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        private List<User> _users = new List<User>() {
            new User(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Mert Demirkiran", "Mert", "Demirkiran", "mertdmkrn37@gmail.com", "kastamonu37")
        };
    }
}
