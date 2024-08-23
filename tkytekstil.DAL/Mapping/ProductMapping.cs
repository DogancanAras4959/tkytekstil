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
    public class ProductMapping : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.ProductSpot).HasMaxLength(500);
            builder.Property(x => x.Price).HasMaxLength(15);
            builder.Property(x => x.ProductBaseImage).HasMaxLength(70).IsRequired();

            builder.HasOne(x => x.brandProduct).WithMany(x => x.productBrandList).HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.categoryProduct).WithMany(x => x.productListCategory).HasForeignKey(x => x.CategoryId);
        }
    }
}
