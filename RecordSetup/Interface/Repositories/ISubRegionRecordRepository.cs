using Application.DTOs;
using RecordSetup.Entities;
using System.Reflection.Emit;

namespace RecordSetup.Interface.Repositories
{
    public interface ISubRegionRecordRepository : IBaseRepository<SubRegionRecord>
    {
        public Task<List<SubRegionRecord>> GetAllSubRegion();
        public Task<SubRegionRecord> GetSubRegion(Guid id);
        Task<IReadOnlyCollection<SelectListItemData>> LoadSubRegionForSelectAsync(string? filter, Guid? id);

    }
}
