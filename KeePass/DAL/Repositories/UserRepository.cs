using DAL.Context;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public async Task<bool> isRegisteredAsync()
        {
            return (await Entities.CountAsync()) == 1;
        }


        public override async Task<User?> UpdateAsync(int id, User entity)
        {
            var user = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(entity.MasterPassword))
                user.MasterPassword = entity.MasterPassword;

            _keyPassContext.Entry(user).State = EntityState.Modified;
            await SaveChangesAsync();
            return user;
        }
    }
}
