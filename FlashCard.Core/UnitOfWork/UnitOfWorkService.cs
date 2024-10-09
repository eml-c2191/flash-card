using FlashCard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly FlashCardDbContext _context;
        public UnitOfWorkService(FlashCardDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangeAsync()
        {
            // TODO: implement try/catch
            UpdateTimestamps();

            await _context.SaveChangesAsync();
        }

        private void UpdateTimestamps()
        {
            var entries = _context.ChangeTracker
            .Entries()
                .Where(e => e.Entity is IHasUpdatedDate && (e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((IHasUpdatedDate)entry.Entity).UpdatedDate = DateTime.Now;
            }
        }
    }
}
