using System.IO;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Contas.Infrastructure.CrossCutting.Identity.Mappings;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contas.Infrastructure.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
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
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
