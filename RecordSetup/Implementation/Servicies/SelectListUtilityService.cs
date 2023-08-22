using Application.DTOs;
using RecordSetup.Interface.Repositories;

namespace RecordSetup.Interface.Servicies;
public class SelectListUtilityService : ISelectListUtilityService
{
    private readonly IRegionRepository _regionService;
    private readonly ISubRegionRecordRepository _subRegionRecordService;
    private readonly ISubRegionRecordTableRepository _subRegionRecordTableService;
    private readonly ITableSchemaRepository _tableSchemaService;


    public SelectListUtilityService(IRegionRepository regionService,
                                    ISubRegionRecordRepository subRegionRecordService,
                                    ISubRegionRecordTableRepository subRegionRecordTableService,
                                    ITableSchemaRepository tableSchemaService)
    {
        _regionService = regionService;
        _subRegionRecordService = subRegionRecordService;
        _subRegionRecordTableService = subRegionRecordTableService;
        _tableSchemaService = tableSchemaService;
    }

    public Task<IReadOnlyCollection<SelectListItemData>> GetRegionsAsync(string? q) =>
        _regionService.LoadRegionForSelectAsync(q);

    public Task<IReadOnlyCollection<SelectListItemData>> GetSubRegionRecordTableAsync(string? q, Guid? id) =>
        _subRegionRecordTableService.LoadSubRegionRecordTableForSelectAsync(q,id);


    public Task<IReadOnlyCollection<SelectListItemData>> GetSubRegionSelectListAsync(string? q) =>
        _subRegionRecordService.LoadSubRegionForSelectAsync(q);

    public Task<IReadOnlyCollection<SelectListItemData>> GetTableSchemaAsync(string? q, Guid? id) =>
        _tableSchemaService.LoadTableSchemaForSelectAsync(q,id);

}
