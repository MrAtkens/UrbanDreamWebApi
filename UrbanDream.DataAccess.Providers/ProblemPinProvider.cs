using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.Contracts.ViewModels.Pins;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models.Pins;
using BazarJok.DataAccess.Models.System;
using BazarJok.DataAccess.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BazarJok.DataAccess.Providers
{
    public class ProblemPinProvider : EntityProvider<ApplicationContext, ProblemPin, Guid>
    {
        private readonly ApplicationContext _context;

        public ProblemPinProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public List<ProblemPin> Search(string text)
        {
            var tag = new Tag {Title = text};
            return _context.ProblemPins.Where(c =>
                text != null && c.Title.Contains(text) ||
                text != null && c.Description.Contains(text) ||
                text != null && c.CreationDate.Equals(DateTime.Parse(text)) ||
                text != null && c.Tags.Contains(tag)).ToList();
        }
        
        public async Task<List<ProblemPin>> GetByUserId(Guid userId)
        {
            return await _context.ProblemPins.Where(x => (x.UserId == userId)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                .ToListAsync();
        }


        public async Task<List<ProblemPin>> GetModeratingPins()
        {
            return await _context.ProblemPins.Where(x => (x.State == PinState.Moderating)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                    .ToListAsync();
        }

        public async Task<List<ProblemPin>> GetAcceptedPins()
        {
            return await _context.ProblemPins.Where(x => (x.State == PinState.Accepted)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                        .ToListAsync();
        }

        public async Task<List<ProblemPin>> GetSolvingPins()
        {
            return await _context.ProblemPins.Where(x => (x.State == PinState.Solving)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                    .ToListAsync();
        }
        
        public async Task<List<ProblemPin>> GetSolvedPins()
        {
            return await _context.ProblemPins.Where(x => (x.State == PinState.Solved)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                         .ToListAsync();
        }

        public async Task<List<ProblemPin>> GetDeniedPins()
        {
            return await _context.ProblemPins.Where(x => (x.State == PinState.Denied)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags)
                      .ToListAsync();
        }
        
        public async Task<ProblemPin> GetFullData(Guid id)
        {
            return await _context.ProblemPins.Where(x => (x.Id == id)).Include(problemPin => problemPin.Images).Include(problemPin => problemPin.Tags).FirstAsync();
        }
    }
}