using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL;
using tkytekstil.ENGINE.Engines;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.Core
{
    internal static class RegisterExtensions
    {
        internal static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var contextConnectionString = configuration.GetConnectionString("Default");
            services.AddDbContextPool<tkytekstildbcontext>(x => x.UseSqlServer(contextConnectionString, o =>
            {
                o.EnableRetryOnFailure(3);
            })
                .EnableSensitiveDataLogging(environment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        internal static void AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ISizeNumService, SizeNumService>();
            services.AddTransient<IShoppersService, ShopperService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IColorProductService, ColorProductService>();
            services.AddTransient<IContactDataService, ContactDataService>();
            services.AddTransient<IImageProductService, ImageProductService>();
            services.AddTransient<ISizeNumberProductService, SizeNumberProductService>();
            services.AddTransient<IProductFavoriteService, ProductFavoriteService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderProductsService, OrderProductService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}
