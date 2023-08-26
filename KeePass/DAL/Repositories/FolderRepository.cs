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
    public class FolderRepository : BaseRepository<Folder>, UpdateEntity<Folder>
    {
        public FolderRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        { }



        public async Task<Folder?> UpdateAsync(int id, Folder updateFolder)
        {
            var folder = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (folder == null) return null;

            if (!string.IsNullOrWhiteSpace(updateFolder.Name))
                folder.Name = updateFolder.Name;

            _keyPassContext.Entry(folder).State = EntityState.Modified;
            await SaveChangesAsync();
            return folder;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await Entities.AnyAsync(x => x.Name == name);
        }

        public override async Task<IEnumerable<Folder>> GetAllAsync()
        {
            return await Entities
                .Include(x=>x.Collections)
                .ToListAsync();
        }

    }
}
