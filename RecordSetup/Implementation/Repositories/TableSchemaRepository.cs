using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using RecordSetup.Context;
using RecordSetup.Entities;
using RecordSetup.Interface.Repositories;

namespace RecordSetup.Implementation.Repositories
{
    public class TableSchemaRepository : BaseRepository<TableSchema>, ITableSchemaRepository
    {
        public TableSchemaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TableSchema>> GetAllTableSchemaAsync()
        {
            return await _context.TableSchemas.Include(x=>x.SubRegionRecordTable)
                .ThenInclude(x => x.SubRegionRecord).ThenInclude(x => x.Region).ToListAsync();
        }
         public async Task<List<TableSchema>> GetAllTableSchemaAsyncByTableId(Guid? id)
        {
            return await _context.TableSchemas.Include(x=>x.SubRegionRecordTable)
                .ThenInclude(x => x.SubRegionRecord).ThenInclude(x => x.Region).Where(x=>x.SubRegionRecordTable.Id == id).ToListAsync();
        }

        public async Task<TableSchema> GetTableSchemaAsync(Guid id)
        {
            return await _context.TableSchemas.Include(x => x.SubRegionRecordTable).ThenInclude(x => x.SubRegionRecord).ThenInclude(x => x.Region).Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<IReadOnlyCollection<SelectListItemData>> LoadTableSchemaForSelectAsync(string? filter, Guid? id)

        {
            IQueryable<TableSchema> query = _context.TableSchemas.Include(x => x.SubRegionRecordTable);
            if (id != null)
            {

                return await query.Where(c => c.SubRegionRecordTableId == id || c.Name.Contains(filter)).OrderBy(c => c.Name)
                  .Select(c => new SelectListItemData(c.Id,
                                                      c.Name,
                                                      c.Name)).AsNoTracking()
                  .ToListAsync();
            }

            return await query
                            .Where(c => filter == null || c.Name.Contains(filter) || c.SubRegionRecordTableId == id).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name))
                            .AsNoTracking()
                            .ToListAsync();
        }
    }
}
