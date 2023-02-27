using HairdresserAppointmentAPI.Helpers;
using HairdresserAppointmentAPI.Model;
using HairdresserAppointmentAPI.Repository.Abstract;
using HairdresserAppointmentAPI.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HairdresserAppointmentAPI.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.email.Equals(email) && x.password.Equals(password.HashString()));
            }
        }

        public async Task<User> SaveUserAsync(User user)
        {
            using (var context = new AppointmentDBContext())
            {
                user.password = user.password.HashString();
                user.createDate = DateTime.Now;
                user.updateDate = user.createDate;

                if(user.fullName.IsNullOrEmpty())
                    user.fullName = string.Join(" ", user.firstName.Trim(), user.lastName.Trim());

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return user;
            }
        }

        public async Task<List<User>> GetUsersAsync() 
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            using (var context = new AppointmentDBContext())
            {
                user.updateDate = DateTime.Now;
                user.fullName = string.Join(" ", user.firstName.Trim(), user.lastName.Trim());

                context.Users.Update(user);
                await context.SaveChangesAsync();
                return user;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            using (var context = new AppointmentDBContext())
            {
                return await context.Users.FindAsync(id);
            }
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            using (var context = new AppointmentDBContext())
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
