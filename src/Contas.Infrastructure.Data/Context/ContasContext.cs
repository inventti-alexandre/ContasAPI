using Contas.Domain.Contas;
using Contas.Domain.Usuarios;
using Contas.Infrastructure.Data.Extensions;
using Contas.Infrastructure.Data.Mappings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contas.Infrastructure.Data.Context
{
    public class ContasContext : DbContext
    {
        private readonly IHostingEnvironment _hostingEnviroment;

        public ContasContext(IHostingEnvironment hostingEnviroment)
        {
            _hostingEnviroment = hostingEnviroment;
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new ContaMapping());
            modelBuilder.AddConfiguration(new CategoriaMapping());
            modelBuilder.AddConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(_hostingEnviroment.ContentRootPath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile($"appsettings.{_hostingEnviroment.EnvironmentName}.json", optional: true).Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}