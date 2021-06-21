using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Providers;
using BazarJok.Tests.DataAccess.infrastructure.Helpers;
using Xunit;

namespace BazarJok.Tests.DataAccess
{
    public class ProductProviderTests
    {
        private ProductProvider _provider;
        
        [Fact]
        public async Task ItShould_GetRightCountOfProducts()
        {
            _provider = new ProductProvider(new DbContextHelper().Context);
            
            var expected = ProductHelper.GetMany().Count();

            var actual = (await _provider.GetAll()).Count;
            
            Assert.True(expected == actual);
        }

        [Fact]
        public async Task ItShould_CreateAProduct()
        {
            var context = new DbContextHelper().Context;
            _provider = new ProductProvider(context);

            var product = new Product
            {
                Title = "CREATE TEST",
                Description = "DETAILS",
                Price = 123,
                Category = new Category
                {
                    Name = "TEST"
                },
                IsRetail = true,
                Images = new List<ProductImage>
                {
                    new ProductImage
                    {
                        Url = "TESTURL1"
                    },
                    new ProductImage()
                    {
                        Url = "TESTURL2"
                    }
                }
            };

            await _provider.Add(product);

            var sut = await _provider.GetById(product.Id);
            
            Assert.NotNull(sut);
            Assert.Equal(product.Title, sut.Title);
            Assert.Equal(product.Category.Name, sut.Category.Name);
            Assert.Equal(product.Images[0].Url, sut.Images[0].Url);
        }
        
        [Fact]
        public async Task ItShould_FindProductsByText()
        {
            var context = new DbContextHelper().Context;
            _provider = new ProductProvider(context);

            var asd = await _provider.GetAll();
            
            var result = await _provider.GetByText("details");
            
            Assert.True(result.Count > 0);
        }
        
    }
}