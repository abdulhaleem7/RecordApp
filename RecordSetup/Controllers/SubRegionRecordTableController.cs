using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class SubRegionRecordTableController : Controller
    {

        private readonly ISubRegionRecordTableService _subRegionRecordTableService;
        private readonly ISubRegionRecordService _subRegionRecordService;
        private readonly INotyfService _notifyService;
        public SubRegionRecordTableController(ISubRegionRecordTableService subRegionRecordTableService,
                                              INotyfService notyfService,
                                              ISubRegionRecordService subRegionRecordService)
        {
            _subRegionRecordTableService = subRegionRecordTableService;
            _notifyService = notyfService;
            _subRegionRecordService = subRegionRecordService;
        }

        [HttpPost]
        [Route("register-sub-region-record_table")]
        public async Task<IActionResult> Register(SubRegionRecordTableRequestModel requestModel)
        {

            try
            {
                var response = await _subRegionRecordTableService.Register(requestModel);
                _notifyService.Custom(response.Message, 10, "white");
                if (response.Status)
                {
                    return RedirectToAction("GetAll");
                }

                return Ok(new
                {
                    status = "error",
                    response.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    message = $"Something happened. Please try again later.{ex.InnerException}"
                });
            }

        }

        [HttpGet]
        [Route("sub-region_record_tables/{id}")]

        public async Task<IActionResult> Get( Guid id)
        {
            try
            {
                var response = await _subRegionRecordTableService.Get(id);

                if (response.Status)
                {
                    _notifyService.Custom(response.Message, 10, "white");
                    return View(response.Data);
                }
                _notifyService.Custom(response.Message, 10, "red");
                return Content(response.Message);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    message = $"Something happened. Please try again later.{ex.InnerException}"
                });
            }

        }
        [Route("sub-region_record_tables")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _subRegionRecordTableService.GetAll();

                if (response.Status)
                {
                    return View(response.Data);
                }
                _notifyService.Custom(response.Message, 10, "red");
                return Content(response.Message);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    message = $"Something happened. Please try again later.{ex.InnerException}"
                });
            }

        }
        [Route("view-Tables/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllBySubRegionId(Guid id)
        {
            try
            {
                var response = await _subRegionRecordService.Get(id);

                if (response.Status)
                {
                    return View(response.Data);
                }
                return Content(response.Message);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    message = $"Something happened. Please try again later.{ex.InnerException}"
                });
            }

        }

        [HttpPost]
        [Route("delete-sub-region_record_tables/{id}")]
        public async Task<IActionResult> Delete( Guid id)
        {
            var result = await _subRegionRecordTableService.Delete(id);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 10, "white");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 10, "red");
            return Content(result.Message);
        }

        [HttpPost("update-subregion_record_tables/{id}")]
        public async Task<IActionResult> Update( Guid id, SubRegionRecordTableRequestModel requestModel)
        {
            var result = await _subRegionRecordTableService.Update(id, requestModel);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 10, "white");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 10, "red");
            Content(result.Message);
            return RedirectToAction();
        }
    }
}
