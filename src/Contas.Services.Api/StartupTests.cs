using AutoMapper;
using Contas.Infrastructure.CrossCutting.AspNetFilters;
using Contas.Infrastructure.CrossCutting.Identity.Context;
using Contas.Services.Api.Configurations;
using Elmah.Io.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Contas.Services.Api
{
    public class StartupTests
    {
        public IConfigurationRoot Configuration { get; }

        public StartupTests(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvcSecurity(Configuration);
            services.AddOptions();

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));

                var police = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(police));
            });

            services.AddApiVersioning("api/v{version}");
            services.AddAutoMapper(typeof(StartupTests));
            services.AddMediatR(typeof(StartupTests));
            services.AddDIConfiguration();

            services.Configure<ElmahIoOptions>(Configuration.GetSection("ElmahIo"));
            services.Configure<ElmahIoOptions>(o =>
            {
                o.OnMessage = message =>
                {
                    message.Version = "v1.0";
                    message.Application = "ContasAPI";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
        {
            app.UseElmahIo();

            #region Configurações MVC

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            #endregion
        }
    }
}
