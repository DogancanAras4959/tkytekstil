using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;

namespace tkytekstil.DAL.Mapping
{
    public class ContactDataMapping : IEntityTypeConfiguration<ContactData>
    {
        public void Configure(EntityTypeBuilder<ContactData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasMaxLength(70);
            builder.Property(x => x.Content);
            builder.Property(x => x.NameSurname).HasMaxLength(75);
            builder.Property(x => x.To).HasMaxLength(50);
            builder.Property(x => x.Subject).HasMaxLength(250);
        }
    }
}
