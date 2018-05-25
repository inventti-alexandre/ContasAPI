using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Contas.Infrastructure.CrossCutting.Identity.Mappings;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contas.Infrastructure.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHostingEnvironment _hostingEnviroment;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHostingEnvironment hostingEnviroment)
            : base(options)
        {
            _hostingEnviroment = hostingEnviroment;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddConfiguration(new ApplicationUserMapping());
            builder.AddConfiguration(new IdentityUserMapping());
            builder.AddConfiguration(new IdentityRoleMapping());
            builder.AddConfiguration(new IdentityUserRoleMapping());
            builder.AddConfiguration(new IdentityUserClaimMapping());
            builder.AddConfiguration(new IdentityUserLoginMapping());
            builder.AddConfiguration(new IdentityUserTokenMapping());
            builder.AddConfiguration(new IdentityRoleClaimMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(_hostingEnviroment.ContentRootPath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile($"appsettings.{_hostingEnviroment.EnvironmentName}.json", optional: true).Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
