using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebshopProject.Models.Entities;

namespace WebshopProject
{
    public class Startup
    {
        private string ConnString { get; set; }

        public Startup(IConfiguration conf)
        {
            ConnString = conf["connString"];
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<WebShopDBContext>(o => o.UseSqlServer(ConnString));
            services.AddDbContext<IdentityDbContext>(o => o.UseSqlServer(ConnString));

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");

            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/ServerError");
            }

            app.UseStatusCodePagesWithRedirects("/Error/HttpError/{0}");

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}
