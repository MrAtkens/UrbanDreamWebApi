using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models;

namespace BazarJok.Tests.DataAccess.infrastructure.Helpers
{
    public static class ProductHelper
    {
        private static Product _exampleOfRetail;
        private static Product _exampleOfNotRetail;

        public static Product GetExampleOfRetail(string id = "11a1a3bb-4131-2c35-bdd5-f16c23ca9769")
        {
            return _exampleOfRetail ??= new Product
            {
                Id = Guid.Parse(id),
                Title = "TEST RETAIL",
                Description = "DETAILS",
                Images = new List<ProductImage>
                {
                    ImageHelper.GetExample()
                },
                Price = 1000,
                Category = ProductTypeHelper.GetFood(),
                IsRetail = true
            };
        }
        
        public static Product GetExampleOfNotRetail(string id = "21a1a3bb-4131-2c35-bdd5-f16c23ca9769")
        {
            return _exampleOfNotRetail ??= new Product
            {
                Id = Guid.Parse(id),
                Title = "TEST NO RETAIL",
                Description = "DETAILS",
                Images = new List<ProductImage>
                {
                    ImageHelper.GetExample()
                },
                Price = 1000,
                Category = ProductTypeHelper.GetFood(),
                IsRetail = false
            };
        }

        public static IEnumerable<Product> GetMany()
        {
            yield return GetExampleOfRetail();
            yield return GetExampleOfNotRetail();
        }
    }
}