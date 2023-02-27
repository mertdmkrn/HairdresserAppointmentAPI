﻿using HairdresserAppointmentAPI.Model;
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

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User> SaveUserAsync(User user)
        {
            return await _userRepository.SaveUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
