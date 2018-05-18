using System;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityUserClaimMapping : EntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.ToTable("IUsersClaims");
        }
    }
}