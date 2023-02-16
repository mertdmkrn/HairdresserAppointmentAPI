using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Service.Abstract;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User UserGetUserByEmailAndPassword(string email, string password)
        {
            using (var context = new AppointmentDBContext())
            {
                return context.Users.FirstOrDefault(x=>x.userEmail.Equals(email) && x.userPassword.Equals(password));
            }
        }

        public IList<User> GetUsers() 
        {
            using (var context = new AppointmentDBContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
