﻿using BLL.Models.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {

        public Task<UserDto?> LoginAsync(string password);
        public Task<UserDto?> RegisterAsync(string password);


        public Task<UserDto?> ChangePassword(int userId, string newPassword);

        public Task<bool> IsRegisteredAsync();
    }
}
