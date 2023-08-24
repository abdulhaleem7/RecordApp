using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SelectListUtilityController : ControllerBase
    {

        private readonly ISelectListUtilityService _selectListUtilityService;
        public SelectListUtilityController(ISelectListUtilityService selectListUtilityService)
        {
            _selectListUtilityService = selectListUtilityService;
        }

        [HttpGet]
        [Route("load-all-region")]
        public async Task<IActionResult> LoadAllRegion([FromQuery] string? q)
        {
            try
            {
                var programss = await _selectListUtilityService.GetRegionsAsync(q);
                return Ok(programss);

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    ex.Message
                });
            }
        } 
        [HttpGet]
        [Route("load-all-subregionrecord")]
        public async Task<IActionResult> LoadAllSubRegionRecord([FromQuery] string? q, Guid? id)
        {
            try
            {
                var programss = await _selectListUtilityService.GetSubRegionSelectListAsync(q, id);
                return Ok(programss);

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    ex.Message
                });
            }
        }
        [HttpGet("load-all-subregionrecordtable")]
        public async Task<IActionResult> LoadAllSubRegionRecordTable([FromQuery] string? q,Guid? id)
        {
            try
            {
               
                
                var programss = await _selectListUtilityService.GetSubRegionRecordTableAsync(q,id);
                return Ok(programss);

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    ex.Message
                });
            }
        }
         [HttpGet("load-all-tableschema")]
        public async Task<IActionResult> LoadAllTableSchemaAsync([FromQuery] string? q,Guid? id)
        {
            try
            {
               
                
                var programss = await _selectListUtilityService.GetTableSchemaAsync(q,id);
                return Ok(programss);

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    ex.Message
                });
            }
        }

    }
}
