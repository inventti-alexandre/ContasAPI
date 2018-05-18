using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class ApplicationUserMapping : EntityTypeConfiguration<ApplicationUser>
    {
        public override void Map(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("IUsers");
        }
    }
}