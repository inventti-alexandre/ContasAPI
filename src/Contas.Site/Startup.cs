using System;
using System.Net;
using AutoMapper;
using Contas.Infrastructure.CrossCutting.AspNetFilters;
using Contas.Infrastructure.CrossCutting.Bus;
using Contas.Infrastructure.CrossCutting.Identity.Data;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Contas.Infrastructure.CrossCutting.IoC;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Contas.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddLogging();
            services.AddMvc(options =>
                {
                    options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter)));
                    options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
                });

            services.AddAutoMapper();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeVisualizar", policy => policy.RequireClaim("Contas", "Visualizar"));
                options.AddPolicy("PodeAlterar", policy => policy.RequireClaim("Contas", "Alterar"));
            });
            RegisterServices(services);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddElmahIo("83ea1721458d44a396e6e00612212252", new Guid("8a070c6a-457c-44c9-9e8b-dbee32a753e9"));
            app.UseElmahIo("83ea1721458d44a396e6e00612212252", new Guid("8a070c6a-457c-44c9-9e8b-dbee32a753e9"));

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conta}/{action=Index}/{id?}");
            });

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services) => Bootstrapper.RegisterServices(services);
    }
}
