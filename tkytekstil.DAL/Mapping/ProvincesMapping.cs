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
    public class ProvincesMapping : IEntityTypeConfiguration<Provinces>
    {
        public void Configure(EntityTypeBuilder<Provinces> builder)
        {
            builder.Property(x => x.ProvinceName).HasMaxLength(50);
            builder.HasOne(x => x.city).WithMany(x => x.provinceList).HasForeignKey(x => x.CityId);
        }
    }
}
