using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Mapping;
using tkytekstil.DAL.Models;

namespace tkytekstil.DAL
{
    public class tkytekstildbcontext : DbContext
    {
        public tkytekstildbcontext(DbContextOptions<tkytekstildbcontext> options) : base(options)
        {

        }

        public DbSet<Brands> brands { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Cities> cities { get; set; }
        public DbSet<ColorProducts> colorProducts { get; set; }
        public DbSet<Colors> colors { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Provinces> provinces { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Shoppers> shoppers { get; set; }
        public DbSet<SizeNum> sizes { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<ContactData> contactDatas { get; set; }
        public DbSet<ImagesProduct> imageProducts { get; set; }
        public DbSet<SizeNumProduct> sizeNumProduct { get; set; }
        public DbSet<ProductFavorite> productFavorite { get; set; }
        public DbSet<OrderProductsDto> orderProducts { get; set; }
        public DbSet<Order> orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new CityMapping());
            modelBuilder.ApplyConfiguration(new ColorMapping());
            modelBuilder.ApplyConfiguration(new ColorProductMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new ProvincesMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new ShoppersMapping());
            modelBuilder.ApplyConfiguration(new SizeMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ContactDataMapping());
            modelBuilder.ApplyConfiguration(new ImagesProductMapping());
            modelBuilder.ApplyConfiguration(new SizeNumProductMapping());
            modelBuilder.ApplyConfiguration(new ProductFavoriteMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());
            modelBuilder.ApplyConfiguration(new OrderProductsMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
