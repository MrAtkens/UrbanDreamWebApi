using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models;

namespace BazarJok.Tests.DataAccess.infrastructure.Helpers
{
    public static class AdminHelper
    {
        private static Admin _developer;

        public static Admin GetDeveloper(string id = "ff51a3bb-0131-4c35-bcd5-f86c23ca9769")
        {
            return _developer ??= new Admin
            {
                Id = Guid.Parse(id),
                Login = "SUPER ADMIN LOGIN",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123123"),
                Role = AdminRole.Developer
            };
        }

        public static IEnumerable<Admin> GetMany()
        {
            yield return GetDeveloper();
        }
    }
}