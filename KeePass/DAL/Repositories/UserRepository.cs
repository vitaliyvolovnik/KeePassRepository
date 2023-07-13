using DAL.Context;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>,UpdateEntity<User>
    {
        public UserRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public async Task<bool> isRegistered()
        {
            return (await Entities.CountAsync()) == 1;
        }


        public async Task<User?> UpdateAsync(int id, User entity)
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
