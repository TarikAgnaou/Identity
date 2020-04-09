using System;
using BLL.Services.Handlers;
using BLL.Services.Requirements;
using DAL.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToolBox.TOs;

namespace LinkendInSecurity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<UserTO, RoleTO>().AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");
            services.AddControllersWithViews();

            services.AddAuthorization(o => 
                {
                    o.AddPolicy("AtLeast21", p => p.Requirements.Add(new MinAgeRequirements(21)));
                });
            services.AddSingleton<IAuthorizationHandler, MinAgeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var rolesManager = serviceProvider.GetService<RoleManager<RoleTO>>();
            var roles = RoleTO.Roles;

            foreach (var roleName in roles)
            {
                var roleExists = rolesManager.RoleExistsAsync(roleName)
                    .GetAwaiter()
                    .GetResult();

                if (!roleExists)
                {
                    rolesManager.CreateAsync(new RoleTO { Name = roleName })
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}
