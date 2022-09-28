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
    public class SizeMapping : IEntityTypeConfiguration<SizeNum>
    {
        public void Configure(EntityTypeBuilder<SizeNum> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SizeNumber).HasMaxLength(3).IsRequired();
        }
    }
}
