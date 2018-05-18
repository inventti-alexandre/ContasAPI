using System;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityUserTokenMapping : EntityTypeConfiguration<IdentityUserToken<string>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.ToTable("IUsersTokens");
        }
    }
}