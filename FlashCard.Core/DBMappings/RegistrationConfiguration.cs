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
    public class RegistrationConfiguration : BaseEntityConfiguration<Registration, int>
    {
        public override void Configure(EntityTypeBuilder<Registration> builder)
        {
            base.Configure(builder);
            builder.ToTable("Registration");
            builder.HasKey(x => x.Id);
        }
    }
}
