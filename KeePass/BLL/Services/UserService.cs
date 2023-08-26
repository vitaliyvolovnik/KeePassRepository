using BLL.Services.Interfaces;
using DAL.Repositories;
using Domain.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepository _userRepository;
        private readonly CryptographyService _cryptographyService;

        public UserService(UserRepository userRepository, CryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }

        public Task<User?> ChangePassword(int userId, string newPassword)
        {
            return _userRepository.UpdateAsync(userId,new User() { MasterPassword = _cryptographyService.HashPassword(newPassword)});
        }

        public async Task<User?> LoginAsync(string password)
        {
            var hashedPass = _cryptographyService.HashPassword(password);
            return await _userRepository.FindFirstAsync(x => x.MasterPassword == hashedPass);
        }

        public async Task<User?> RegisterAsync(string password)
        {
            if(! await _userRepository.isRegisteredAsync())
            {
                var hashedPass = _cryptographyService.HashPassword(password);
                return await _userRepository.CreateAsync(new User
                {
                    MasterPassword = hashedPass
                });
            }
            return null;
        }

        public async Task<bool> isRegisteredAsync()
        {
            return await _userRepository.isRegisteredAsync();
        }
    }
}
