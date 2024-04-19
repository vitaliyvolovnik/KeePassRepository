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
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(KeyPassContext keyPassContext) : base(keyPassContext)
        {
        }

        public override async Task<Note?> UpdateAsync(int id, Note entity)
        {
            var note = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null) return null;

            if (!string.IsNullOrWhiteSpace(entity.Name))
                note.Name = entity.Name;

            if (!string.IsNullOrWhiteSpace(entity.Password))
                note.Password = entity.Password;

            _keyPassContext.Entry(note).State = EntityState.Modified;
            await SaveChangesAsync();
            return note;
        }
    }
}
