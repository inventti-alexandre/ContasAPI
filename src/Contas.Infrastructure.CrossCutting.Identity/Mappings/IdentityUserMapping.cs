using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityUserMapping : EntityTypeConfiguration<IdentityUser>
    {
        public override void Map(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.ToTable("IUsers");
        }
    }
}