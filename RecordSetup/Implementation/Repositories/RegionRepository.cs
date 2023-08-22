using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using RecordSetup.Context;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;
using System.Reflection.Emit;

namespace RecordSetup.Implementation.Repositories
{
    public class RegionRepository:BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<SelectListItemData>> LoadRegionForSelectAsync(string? filter) =>
            await _context.Regions
                            .Where(c => filter == null || c.Name.Contains(filter)).OrderBy(c => c.Name)
                             .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name))
                            .AsNoTracking()
                            .ToListAsync();
    }
}
