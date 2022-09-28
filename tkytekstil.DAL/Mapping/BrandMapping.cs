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
    public class BrandMapping : IEntityTypeConfiguration<Brands>
    {
        public void Configure(EntityTypeBuilder<Brands> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BrandName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BrandImage).HasMaxLength(50).IsRequired();
        }
    }
}
