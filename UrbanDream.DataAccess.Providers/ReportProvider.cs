using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Models;
using BazarJok.DataAccess.Providers.Abstract;

namespace BazarJok.DataAccess.Providers
{
    public class ReportProvider : EntityProvider<ApplicationContext, Report, Guid>
    {
        private readonly ApplicationContext _context;

        public ReportProvider(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Report> GetByName(string login)
        {
            return await FirstOrDefault(x => x.Name.Equals(login)); //?? throw new ArgumentException("Report is not found");
        }

        public virtual async Task<List<Report>> GetByBrigadeId(Guid brigadeId)
        {
            return (await GetAll()).Where(x =>
                x.BrigadeId.Equals(brigadeId)
            ).ToList();
        }
        
        public virtual async Task<int> GetCountOfReport(Guid brigadeId)
        {
            return (await GetAll()).Count(x => x.BrigadeId.Equals(brigadeId));
        }
    }
}