using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityRoleMapping : EntityTypeConfiguration<IdentityRole>
    {
        public override void Map(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("IRoles");
        }
    }
}