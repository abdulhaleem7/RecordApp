using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using RecordSetup.Context;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;

namespace RecordSetup.Implementation.Repositories
{
    public class SubRegionRecordTableRepository : BaseRepository<SubRegionRecordTable>, ISubRegionRecordTableRepository
    {
        public SubRegionRecordTableRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<SubRegionRecordTable>> GetAllSubregionRecordTable()
        {
            return await _context.SubRegionRecordTables.Include(x=>x.SubRegionRecord).ToListAsync();
        }

        public async Task<SubRegionRecordTable> GetSubregionRecordTable(Guid id)
        {
            return await _context.SubRegionRecordTables.Include(x => x.SubRegionRecord).Where(x => x.Id == id).FirstOrDefaultAsync();
        }


			

        public async Task<IReadOnlyCollection<SelectListItemData>> LoadSubRegionRecordTableForSelectAsync(string? filter, Guid? id)

        {
            IQueryable<SubRegionRecordTable> query = _context.SubRegionRecordTables.Include(x => x.SubRegionRecord);
            if (id != null)
            {
               
                          return await query.Where(c => c.SubRegionRecordId == id || c.Name.Contains(filter)).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name)).AsNoTracking()
                            .ToListAsync();
            }

            return await query
                            .Where(c => filter == null || c.Name.Contains(filter) || c.SubRegionRecord.Id == id).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name))
                            .AsNoTracking()
                            .ToListAsync();
        }
    }
}
