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
    public class ColorProductMapping : IEntityTypeConfiguration<ColorProducts>
    {
        public void Configure(EntityTypeBuilder<ColorProducts> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.products).WithMany(x => x.productList).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.color).WithMany(x => x.colorsList).HasForeignKey(x => x.ColorId);
        }
    }
}
