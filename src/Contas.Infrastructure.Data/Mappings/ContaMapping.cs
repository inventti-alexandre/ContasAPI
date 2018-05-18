using Contas.Domain.Contas;
using Contas.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Data.Mappings
{
    public class ContaMapping : EntityTypeConfiguration<Conta>
    {
        public override void Map(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");
            builder.Property(conta => conta.Data).IsRequired().HasColumnType("date");
            builder.Property(conta => conta.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(conta => conta.NumeroParcelas).HasDefaultValue(0);
            builder.Property(conta => conta.Observacao).HasColumnType("varchar(max)");
            builder.Property(conta => conta.Parcelado).IsRequired();
            builder.Property(conta => conta.Valor).IsRequired();
            builder.Property(conta => conta.Desativado).IsRequired().HasDefaultValue(false);

            //Relacionamentos
            builder.HasOne(conta => conta.Usuario).WithMany(usuario => usuario.Contas).HasForeignKey(conta => conta.IdUsuario);
            builder.HasOne(conta => conta.Categoria).WithMany(categoria => categoria.Contas).HasForeignKey(conta => conta.IdCategoria);

            builder.Ignore(conta => conta.ValidationResult);
            builder.Ignore(conta => conta.CascadeMode);
        }
    }
}