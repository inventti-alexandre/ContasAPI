using System;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityRoleClaimMapping : EntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public override void Map(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.ToTable("IRolesClaims");
        }
    }
}