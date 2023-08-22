using Application.DTOs;
using RecordSetup.Entities;
using System.Reflection.Emit;

namespace RecordSetup.Interface.Repositories
{
    public interface ITableSchemaRepository : IBaseRepository<TableSchema>
    {
        Task<List<TableSchema>> GetAllTableSchemaAsync();
        Task<TableSchema> GetTableSchemaAsync(Guid id);
        Task<IReadOnlyCollection<SelectListItemData>> LoadTableSchemaForSelectAsync(string? filter,Guid? id);

    }
}
