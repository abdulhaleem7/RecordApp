using Application.DTOs;
using RecordSetup.Entities;

namespace RecordSetup.Interface.Repositories
{
    public interface IRegionRepository :IBaseRepository<Region>
    {

        Task<IReadOnlyCollection<SelectListItemData>> LoadRegionForSelectAsync(string? filter);
    }
}
