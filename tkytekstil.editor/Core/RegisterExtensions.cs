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
using tkytekstil.CORE.UnitOfWork;
using tkytekstil.ENGINE.Engines;
using tkytekstil.ENGINE.Interface;

using tkytekstil.DAL;

namespace tkytekstil.editor.Core
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
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ISizeNumService, SizeNumService>();
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient(typeof(IEmailSender), typeof(EmailSender));
            services.AddTransient<IShoppersService, ShopperService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IImageProductService, ImageProductService>();
            services.AddTransient<ISizeNumberProductService, SizeNumberProductService>();
            services.AddTransient<IColorProductService, ColorProductService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddTransient<IContactDataService, ContactDataService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderProductsService, OrderProductService>();
        }
    }
}
