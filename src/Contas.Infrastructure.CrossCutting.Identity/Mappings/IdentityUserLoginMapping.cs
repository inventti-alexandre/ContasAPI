using System;
using Contas.Infrastructure.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.CrossCutting.Identity.Mappings
{
    public class IdentityUserLoginMapping : EntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public override void Map(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.ToTable("IUsersLogins");
        }
    }
}