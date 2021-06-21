using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models;

namespace BazarJok.Tests.DataAccess.infrastructure.Helpers
{
    public static class ProductTypeHelper
    {
        private static Category _food;

        public static Category GetFood(string id = "ffa1a3bb-4131-2c35-bdd5-f16c23ca9769")
        {
            return _food ??= new Category
            {
                Id = Guid.Parse(id),
                Name = "FOOD"
            };
        }

        public static IEnumerable<Category> GetMany()
        {
            yield return GetFood();
        }
    }
}