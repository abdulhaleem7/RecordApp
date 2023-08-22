using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordSetup.Interface.Servicies
{
	public interface ISelectListUtilityService
	{
		Task<IReadOnlyCollection<SelectListItemData>> GetRegionsAsync(string? q);
		Task<IReadOnlyCollection<SelectListItemData>> GetSubRegionSelectListAsync(string? q);
        Task<IReadOnlyCollection<SelectListItemData>> GetSubRegionRecordTableAsync(string? q,Guid? id);
		Task<IReadOnlyCollection<SelectListItemData>> GetTableSchemaAsync(string? q,Guid? id);
    }
}
