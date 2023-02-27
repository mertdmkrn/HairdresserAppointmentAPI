﻿using HairdresserAppointmentAPI.Model;

namespace HairdresserAppointmentAPI.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task<User> SaveUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> GetUserById(int id);
        Task<bool> DeleteUserAsync(User user);
    }
}
