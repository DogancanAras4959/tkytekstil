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
    public class ProductFavoriteMapping : IEntityTypeConfiguration<ProductFavorite>
    {
        public void Configure(EntityTypeBuilder<ProductFavorite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.product).WithMany(x => x.productFavoriteList).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.shoppers).WithMany(x => x.productFavoriteList).HasForeignKey(x => x.ShopperId);
        }
    }
}
