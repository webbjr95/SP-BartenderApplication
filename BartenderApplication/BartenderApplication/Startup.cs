using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BartenderApplication.Data;
using BartenderApplication.Models;
using System;
using System.Threading.Tasks;

namespace BartenderApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private async Task CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            //Create managers to handle adding roles and the user to the database
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Bartender", "Standard" };
            IdentityResult roleCreateResult;

            //Create the roles from our array if they do not exist within the database
            foreach (var roleName in roleNames)
            {
                var doesRoleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!doesRoleExist)
                {
                    roleCreateResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Setup admin account
            var adminUserName = "System Admin";
            var adminEmail = "admin@rnyc.com";
            var adminFirstName = "System";
            var adminLastName = "Admin";
            var adminPassword = "admin";

            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                FirstName = adminFirstName,
                LastName = adminLastName
            };

            //Check if the user is already present within the database, if not then create it and assign to the admin role.
            var doesAdminUserExist = await UserManager.FindByNameAsync(adminUserName);
            if (doesAdminUserExist == null)
            {
                var createAdminUser = await UserManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    //Tie the admin account to the admin role
                    await UserManager.AddToRoleAsync(adminUser, "Bartender");
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DrinkMenuDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<OrdersPlacedDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider config)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRolesAndAdminUser(config).Wait();
        }
    }
}
