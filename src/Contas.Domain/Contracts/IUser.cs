using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Contas.Domain.Contracts
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}