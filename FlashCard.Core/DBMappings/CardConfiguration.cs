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
    public class CardConfiguration : BaseEntityConfiguration<Card, int>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);
            builder.ToTable("Appointment");
            builder.Property(i => i.Date).HasColumnName("CardDate");
            builder.Property(i => i.Time).HasColumnName("CardTime");
            builder.Property(i => i.CardType).IsUnicode(false);
            builder.Property(i => i.Address).IsUnicode(false);
            builder.Property(i => i.Content).IsUnicode(false);
            builder.Property(i => i.Title).IsUnicode(false);
        }
    }
}
