using DAL.Context;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CollectionRepository : BaseRepository<Collection>, UpdateEntity<Collection>
    {
        public CollectionRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public async Task<Collection?> UpdateAsync(int id, Collection entity)
        {
            var collection = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (collection == null) return null;

            if (!string.IsNullOrWhiteSpace(entity.Name))
                collection.Name = entity.Name;

            _keyPassContext.Entry(collection).State = EntityState.Modified;
            await SaveChangesAsync();
            return collection;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await Entities.AnyAsync(x => x.Name == name);
        }
    }
}
