using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GetToTheShopper.WebApi.Models;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GetToTheShopper.WebApi.Validation;

namespace GetToTheShopper.WebApi
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
            services.AddCors();


            Action<DbContextOptionsBuilder> opt = options =>
                options.UseSqlServer(Configuration.GetConnectionString("GetToTheShopperContext"));



            services.AddDbContext<GetToTheShopperContext>(opt);
            
            services.AddMvc(options => 
            {
                options.Filters.Add(new ValidateModel());
                options.Filters.Add(new ValidateExceptions());
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GetToTheShopperContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                //// Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
            CreateRoles(serviceProvider);
        }


        private void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;

            //Check that there is an Seller role and create if not
            Task<bool> hasSellerRole = roleManager.RoleExistsAsync("Seller");
            hasSellerRole.Wait();

            if (!hasSellerRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Seller"));
                roleResult.Wait();
            }

            //Check that there is an Client role and create if not
            Task<bool> hasClientRole = roleManager.RoleExistsAsync("Client");
            hasClientRole.Wait();

            if (!hasClientRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Client"));
                roleResult.Wait();
            }
        }
    }

    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Action<DbContextOptionsBuilder> opt = options =>
                options.UseInMemoryDatabase("MyLocalDB");

            services.AddDbContext<GetToTheShopperContext>(opt);
            services.AddMvc(options => { options.Filters.Add(new ValidateModel()); });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GetToTheShopperContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //// Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc();
        }


        private void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;

            //Check that there is an Seller role and create if not
            Task<bool> hasSellerRole = roleManager.RoleExistsAsync("Seller");
            hasSellerRole.Wait();

            if (!hasSellerRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Seller"));
                roleResult.Wait();
            }

            //Check that there is an Client role and create if not
            Task<bool> hasClientRole = roleManager.RoleExistsAsync("Client");
            hasClientRole.Wait();

            if (!hasClientRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Client"));
                roleResult.Wait();
            }
        }
    }
}
