using Application.DTOs;
using RecordSetup.Entities;
using System.Reflection.Emit;

namespace RecordSetup.Interface.Repositories
{
    public interface ISubRegionRecordTableRepository : IBaseRepository<SubRegionRecordTable>
    {
        Task<List<SubRegionRecordTable>> GetAllSubregionRecordByRegionIdTable(Guid? id);
        Task<List<SubRegionRecordTable>> GetAllSubregionRecordTable();
        Task<SubRegionRecordTable> GetSubregionRecordTable(Guid id);
        Task<IReadOnlyCollection<SelectListItemData>> LoadSubRegionRecordTableForSelectAsync(string? filter,Guid? id);



    }
}
