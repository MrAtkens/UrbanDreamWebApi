using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Models.System;
using BazarJok.DataAccess.Providers.Abstract;

namespace BazarJok.DataAccess.Providers
{
    public class ReportImageProvider : EntityProvider<ApplicationContext, ReportImage, Guid>
    {
        public ReportImageProvider(ApplicationContext context) : base(context)
        {
        }

        public virtual async Task<List<ReportImage>> AddToDatabaseAndGetAll(List<string> photos)
        {
            var result = new List<ReportImage>();
            
            foreach (var url in photos)
            {
                var photo = await FirstOrDefault(x => x.Url.Equals(url));

                if (photo is null)
                {
                    photo = new ReportImage
                    {
                        Url = url
                    };

                    await Add(photo);
                }

                result.Add(photo);
            }

            return result;
        }
    }
}