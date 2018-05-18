using System;
using System.Collections.Generic;
using System.Security.Claims;
using Contas.Domain.Contracts;
using Microsoft.AspNetCore.Http;

namespace Contas.Infrastructure.CrossCutting.Identity.Models
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public User(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() 
                ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) 
                : Guid.NewGuid();
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}