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
    public class CategoryMapping : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryDescription);
            builder.Property(x => x.CategoryImage).HasMaxLength(40).IsRequired();
            builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
        }
    }
}
