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
    public class SizeNumProductMapping : IEntityTypeConfiguration<SizeNumProduct>
    {
        public void Configure(EntityTypeBuilder<SizeNumProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.product).WithMany(x => x.sizeNumProduct).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.size).WithMany(x => x.sizeNumProductList).HasForeignKey(x => x.SizeId);
        }
    }
}
