using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}