using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using tkytekstil.Core;
using tkytekstil.Core.AuthorizationHandler;
using tkytekstil.CORE.EmailConfig;
using tkytekstil.ENGINE.Mapper;

namespace tkytekstil
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options => { options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); });

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddDbContextDI(_configuration, Environment);
            services.AddInjections();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {

                options.IdleTimeout = TimeSpan.FromMinutes(30);

            });

            services.AddAuthentication("FormAuthenticationWithCookie")
            .AddCookie("FormAuthenticationWithCookie", config =>
            {
                config.Cookie.Name = "form-authentication-with-cookie";
                config.LoginPath = "/bayi/girisyap";
                config.AccessDeniedPath = "/bayi/girisyap";
            });

            //for authorization
            services.AddAuthorization(config =>
            {
                config.AddPolicy("UserPolicy", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim(ClaimTypes.Email);
                    policyBuilder.UserRequireCustomClaim(ClaimTypes.Name);
                });

            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneralMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<EmailConfiguration>(_configuration.GetSection("EmailConfiguration"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=anasayfa}/{action=sayfa}/{id?}");

               endpoints.MapControllerRoute(
               name: "detay",
               pattern: "/detay/{id}/{productname}", new { controller = "urun", action = "detay" });
            });
        }
    }
}
