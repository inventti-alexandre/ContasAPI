using Contas.Domain.Contas;
using Contas.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Data.Mappings
{
    public class CategoriaMapping : EntityTypeConfiguration<Categoria>
    {
        public override void Map(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.Property(categoria => categoria.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(categoria => categoria.Descricao).IsRequired().HasColumnType("varchar(200)");

            builder.Ignore(categoria => categoria.ValidationResult);
            builder.Ignore(categoria => categoria.CascadeMode);
        }
    }
}