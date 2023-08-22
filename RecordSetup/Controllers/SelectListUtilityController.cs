using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{

    [Route("api/utilities"), ApiController]
    public class SelectListUtilityController : ControllerBase
    {

        private readonly ISelectListUtilityService _selectListUtilityService;
        public SelectListUtilityController(ISelectListUtilityService selectListUtilityService)
        {
            _selectListUtilityService = selectListUtilityService;
        }

        [HttpGet("load-all-Region")]
        public async Task<IActionResult> LoadAllRegionAsync([FromQuery] string? q)
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
        [HttpGet("load-all-SubRegionRecord")]
        public async Task<IActionResult> LoadAllSubRegionRecordAsync([FromQuery] string? q)
        {
            try
            {
               
                
                var programss = await _selectListUtilityService.GetSubRegionSelectListAsync(q);
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
        [HttpGet("load-all-SubRegionRecordTGable")]
        public async Task<IActionResult> LoadAllSubRegionRecordTableAsync([FromQuery] string? q,Guid? id)
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
         [HttpGet("load-all-TableSchema")]
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
