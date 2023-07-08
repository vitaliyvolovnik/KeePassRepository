using DAL.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public async Task<bool> isRegistered()
        {
            return (await Entities.CountAsync()) == 1;
        }


        public async Task<User?> ChangePassword()
        {
            throw new NotImplementedException();
        }
    }
}
