using System;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BazarJok.Tests.DataAccess.infrastructure.Helpers
{
    public class DbContextHelper
    {
        public ApplicationContext Context { get; set; }
        
        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase("UNIT_TESTING_" + Guid.NewGuid());

            var options = builder.Options;

            Context = new ApplicationContext(options);
            
            // Add test data there
            
            Context.AddRange(AdminHelper.GetMany());

            Context.AddRange(ImageHelper.GetMany());
            
            Context.AddRange(ProductTypeHelper.GetMany());
            
            Context.AddRange(ProductHelper.GetMany());
            
            Context.SaveChanges();
        }
    }
}