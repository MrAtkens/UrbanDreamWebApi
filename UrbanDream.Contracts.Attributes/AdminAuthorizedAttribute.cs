using System;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Models.Users;
using Microsoft.AspNetCore.Authorization;


namespace BazarJok.Contracts.Attributes
{
    public sealed class AdminAuthorizedAttribute : AuthorizeAttribute
    {
        
        public AdminAuthorizedAttribute(params AdminRole[] roles) : base()
        {
            Roles = String.Join(",", roles);
        }

    }

}
