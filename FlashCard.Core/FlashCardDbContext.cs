using FlashCard.Core.DBMappings;
using FlashCard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core
{
    public partial class FlashCardDbContext : DbContext
    {
        public FlashCardDbContext(DbContextOptions<FlashCardDbContext> options) : base(options)
        {
        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfiguration();
        }
    }
}
