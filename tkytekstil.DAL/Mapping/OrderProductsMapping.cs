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
    public class OrderProductsMapping : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.order).WithMany(x => x.orderProducts).HasForeignKey(x => x.OrderId);
            builder.Property(x => x.Size).HasMaxLength(3);
            builder.Property(x => x.Color).HasMaxLength(15);
            builder.HasOne(x => x.product).WithMany(x => x.orderProducts).HasForeignKey(x => x.ProductId);
        }
    }
}
