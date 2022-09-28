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
    public class ColorMapping : IEntityTypeConfiguration<Colors>
    {
        public void Configure(EntityTypeBuilder<Colors> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ColorCode).HasMaxLength(8).IsRequired();
            builder.Property(x => x.ColorName).HasMaxLength(35).IsRequired();
        }
    }
}
