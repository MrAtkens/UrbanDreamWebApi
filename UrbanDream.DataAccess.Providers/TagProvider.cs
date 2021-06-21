using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Providers.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.DataAccess.Models.System;

namespace BazarJok.DataAccess.Providers
{
    public class TagProvider : EntityProvider<ApplicationContext, Tag, Guid>
    {
        private readonly ApplicationContext _context;

        public TagProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<List<Tag>> GetAllAndCreateIfNotContains(List<string> tags)
        {
            var result = new List<Tag>();

            foreach (var title in tags)
            {
                var tag = await GetByTitle(title);

                if (tag is null)
                {
                    tag = new Tag
                    {
                        Title = title
                    };

                    await Add(tag);
                }
                
                result.Add(tag);
            }

            return result;
        }
        
        public virtual async Task<Tag> GetByTitle(string title)
        {
            var result = await _context.Tags.Where(x => x.Title.ToLower().Equals(title.ToLower()))
                .FirstOrDefaultAsync();
            return result;
        }
        public virtual async Task<List<Tag>> GetByPartOfWord(string word)
        {
            var result = await _context.Tags
                .Where(x => x.Title.ToLower().Contains(word)).ToListAsync();

            return result;
        }


    }
}
