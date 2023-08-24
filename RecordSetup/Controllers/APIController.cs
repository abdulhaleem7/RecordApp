using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly ISubRegionRecordService _subRegionRecordService;
        private readonly ISubRegionRecordTableService _subRegionRecordTableService;
        private readonly ITableSchemaService _tableSchemaService;
        private readonly INotyfService _notifyService; 
        public APIController(IRegionService regionService, ISubRegionRecordService subRegionRecordService, ISubRegionRecordTableService subRegionRecordTableService, ITableSchemaService tableSchemaService, INotyfService notifyService)
        {
            _regionService = regionService;
            _subRegionRecordService = subRegionRecordService;
            _subRegionRecordTableService = subRegionRecordTableService;
            _tableSchemaService = tableSchemaService;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> GetRegions()
        {
            
            var response = await _regionService.GetAll();
            if(response.Status) _notifyService.Custom(response.Message, 10, "white");
            return Ok(new
            {
                message = response.Message,
                data = response.Data,
                status = response.Status
            });
        }
        [Route("sub-regions")]
        public async Task<IActionResult> GetSubRegionRecords()
        {
            
            var response = await _subRegionRecordService.GetAll();
            if(response.Status) _notifyService.Custom(response.Message, 10, "white");
            return Ok(new
            {
                message = response.Message,
                data = response.Data,
                status = response.Status
            });
        }
        
        [Route("sub-region-record-tables")]
        public async Task<IActionResult> GetSubRegionRecordTables()
        {
            
            var response = await _subRegionRecordTableService.GetAll();
            if(response.Status) _notifyService.Custom(response.Message, 10, "white");
            return Ok(new
            {
                message = response.Message,
                data = response.Data,
                status = response.Status
            });
        } 
        
        [Route("table-schemas_api")]
        public async Task<IActionResult> GetTableSchemas()
        {
            
            var response = await _tableSchemaService.GetAll();
            if(response.Status) _notifyService.Custom(response.Message, 10, "white");
            return Ok(new
            {
                message = response.Message,
                data = response.Data,
                status = response.Status
            });
        }
    }
}
