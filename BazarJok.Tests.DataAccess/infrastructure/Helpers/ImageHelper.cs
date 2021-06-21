using System;
using System.Collections.Generic;
using BazarJok.DataAccess.Models;

namespace BazarJok.Tests.DataAccess.infrastructure.Helpers
{
    public static class ImageHelper
    {
        private static ProductImage _example;

        public static ProductImage GetExample(string id = "faa1a2aa-4131-2c35-bdd5-f1ba23ca9749")
        {
            return _example ??= new ProductImage
            {
                Id = Guid.Parse(id),
                Url = "https://trello-members.s3.amazonaws.com/" +
                      "5c502da60c74f988516a3899/15a488b1c4483dea181818f9623c495c/original.png"
            };
        }

        public static IEnumerable<ProductImage> GetMany()
        {
            yield return GetExample();
        }
    }
}