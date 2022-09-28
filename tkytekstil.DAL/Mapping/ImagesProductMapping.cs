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
    public class ImagesProductMapping : IEntityTypeConfiguration<ImagesProduct>
    {
        public void Configure(EntityTypeBuilder<ImagesProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasMaxLength(75);
            builder.HasOne(x => x.product).WithMany(x => x.imagesProductList).HasForeignKey(x => x.ProductId);
        }
    }
}
