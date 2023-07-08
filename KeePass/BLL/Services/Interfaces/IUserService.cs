using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {

        public Task<User?> LoginAsync(string password);
        public Task<User?> RegisterAsync(string password);


        public Task<User?> ChangePassword(int userId, string newPassword);
    }
}
