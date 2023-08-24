using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using RecordSetup.Context;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;

namespace RecordSetup.Implementation.Repositories
{
    public class SubRegionRecordRepository : BaseRepository<SubRegionRecord>, ISubRegionRecordRepository
    {
        public SubRegionRecordRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<SubRegionRecord>> GetAllSubRegion()
        {
            return await _context.SubRegionRecords.Include(x => x.Region).ToListAsync();
        }

        public async Task<SubRegionRecord> GetSubRegion(Guid id)
        {
            return await _context.SubRegionRecords.Include(x => x.Region).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<SelectListItemData>> LoadSubRegionForSelectAsync(string? filter, Guid? id) 
            
        {   

            if(id != null)
            {
                return await _context.SubRegionRecords
                            .Where(c => c.RegionId == id || c.Name.Contains(filter)).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name)).AsNoTracking()
                            .ToListAsync();
            }
            
            return await _context.SubRegionRecords
                            .Where(c => filter == null || c.RegionId == id || c.Name.Contains(filter)).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name)).AsNoTracking()
                            .ToListAsync();
         }
    }
}
