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
    public class ShoppersMapping : IEntityTypeConfiguration<Shoppers>
    {
        public void Configure(EntityTypeBuilder<Shoppers> builder)
        {
            builder.Property(x => x.ShopperName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.ShopperEmail).HasMaxLength(70).IsRequired();
            builder.Property(x => x.ShopperPhone).HasMaxLength(70);
            builder.Property(x => x.ShopperAddress).HasMaxLength(70);
            builder.Property(x => x.ShopperIdentityNumber).HasMaxLength(11);
            builder.Property(x => x.ShopperSurname).HasMaxLength(200);
            builder.Property(x => x.ShopperAddress).HasMaxLength(70);
            builder.Property(x => x.ShopperUserName).HasMaxLength(70);
            builder.Property(x => x.ShopperPassword).HasMaxLength(20);
            builder.HasOne(x => x.provinceShopper).WithMany(x => x.shopperList).HasForeignKey(x => x.ProvinceId);
            builder.HasOne(x => x.cityShopper).WithMany(x => x.shopperList).HasForeignKey(x => x.CityId);
            builder.Property(x => x.CompanyName).HasMaxLength(100);
        }
    }
}
