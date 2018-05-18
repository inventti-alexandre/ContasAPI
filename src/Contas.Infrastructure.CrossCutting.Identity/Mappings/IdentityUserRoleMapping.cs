using System;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityUserRoleMapping : EntityTypeConfiguration<IdentityUserRole<string>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.ToTable("IUsersRoles");
        }
    }
}