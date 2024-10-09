using FlashCard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.DBMappings
{
    public abstract class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedDate)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .IsRequired();

            builder.Property(e => e.DeletedDate).HasDefaultValueSql(null);

            builder.Property(e => e.UpdatedDate)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.UpdatedBy)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.DeletedBy)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
