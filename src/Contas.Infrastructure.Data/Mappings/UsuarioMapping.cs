using Contas.Domain.Contas;
using Contas.Domain.Usuarios;
using Contas.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Data.Mappings
{
    public class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.Property(usuario => usuario.Nome).IsRequired().HasColumnType("varchar(50)");
            builder.Property(usuario => usuario.Sobrenome).IsRequired().HasColumnType("varchar(50)");
            builder.Property(usuario => usuario.Email).IsRequired().HasColumnType("varchar(150)");
            builder.Property(usuario => usuario.CPF).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");
            builder.Property(usuario => usuario.DataNascimento).IsRequired().HasColumnType("date");

            builder.Ignore(usuario => usuario.ValidationResult);
            builder.Ignore(usuario => usuario.CascadeMode);
        }
    }
}