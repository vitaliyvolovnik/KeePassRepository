using DAL.Context;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public override async Task<Collection?> UpdateAsync(int id, Collection entity)
        {
            Collection collection;
            try
            {
                collection = await Entities.FirstAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(entity.Name))
                collection.Name = entity.Name;

            _keyPassContext.Entry(collection).State = EntityState.Modified;
            await SaveChangesAsync();
            return collection;
        }

        public override async Task<Collection?> FindFirstAsync(Expression<Func<Collection, bool>> predicate)
        {
            try
            {
                return await Entities
                .Include(x => x.Notes)
                .FirstAsync(predicate)
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await Entities.AnyAsync(x => x.Name == name);
        }
    }
}
