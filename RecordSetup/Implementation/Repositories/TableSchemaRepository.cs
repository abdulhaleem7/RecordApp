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
            return await _context.TableSchemas.Include(x=>x.SubRegionRecordTable).ToListAsync();
        }

        public async Task<TableSchema> GetTableSchemaAsync(Guid id)
        {
            return await _context.TableSchemas.Include(x => x.SubRegionRecordTable).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<SelectListItemData>> LoadTableSchemaForSelectAsync(string? filter, Guid? id) =>
            await _context.TableSchemas.Include(x => x.SubRegionRecordTable)
                            .Where(c => filter == null || c.Name.Contains(filter) || c.SubRegionRecordTable.Id == id).OrderBy(c => c.Name)
                            .Select(c => new SelectListItemData(c.Id,
                                                                c.Name,
                                                                c.Name))
                            .AsNoTracking()
                            .ToListAsync();
    }
}
