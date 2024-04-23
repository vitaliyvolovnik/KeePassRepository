using BLL.Extensions;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Domain.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly CryptographyService _cryptographyService;

        public UserService(IUserRepository userRepository, CryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task<UserDto?> ChangePassword(int userId, string newPassword)
        {
            var user = await _userRepository.UpdateAsync(userId,new User() { MasterPassword = _cryptographyService.HashPassword(newPassword)});
            return user?.ToDto(_cryptographyService);
        }

        public async Task<UserDto?> LoginAsync(string password)
        {
            var hashedPass = _cryptographyService.HashPassword(password);
            return (await _userRepository.FindFirstAsync(x => x.MasterPassword == hashedPass))?.ToDto(_cryptographyService);
        }

        public async Task<UserDto?> RegisterAsync(string password)
        {
            if(! await _userRepository.isRegisteredAsync())
            {
                var hashedPass = _cryptographyService.HashPassword(password);
                var user =  await _userRepository.CreateAsync(new User
                {
                    MasterPassword = hashedPass
                });
                return user?.ToDto(_cryptographyService);
            }
            return null;
        }

        public async Task<bool> isRegisteredAsync()
        {
            return await _userRepository.isRegisteredAsync();
        }
    }
}
