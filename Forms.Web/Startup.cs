using System;
using Forms.Core.Options;
using Forms.Infrastructure;
using Forms.Web.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Forms.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private bool IsDevelopment { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment appEnv)
        {
            Configuration = configuration;
            IsDevelopment = appEnv.IsDevelopment();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.Cookie.HttpOnly = true;
                x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                x.LoginPath = "/";
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            
            services.AddControllersWithViews(x =>
            {
                x.Filters.Add(new GlobalExceptionFilter());
            });
            
            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromDays(365);
            });

            // Services
            services.AddHttpContextAccessor();
            services.AddCoreServices(Configuration);
            
            // Infrastructure
            var redisConnectionFactory = services.AddCacheFramework(Configuration, IsDevelopment);
            services.AddEmailFramework(Configuration);

            // Data protection keys
            services.AddDataProtection().PersistKeysToStackExchangeRedis(redisConnectionFactory.GetDatabase().Multiplexer, "DataProtection-Keys");
            
            // Options
            services.Configure<FormOptions>(Configuration.GetSection("Forms"));
        }

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

            app.UseStatusCodePagesWithRedirects("~/StatusCode/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}