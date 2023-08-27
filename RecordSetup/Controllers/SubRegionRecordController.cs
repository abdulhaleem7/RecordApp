using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class SubRegionRecordController : Controller
    {

        private readonly ISubRegionRecordService _subRegionRecordService;
        private readonly INotyfService _notifyService;
        public SubRegionRecordController(ISubRegionRecordService subRegionRecordService, INotyfService notyfService)
        {
            _subRegionRecordService = subRegionRecordService;
            _notifyService = notyfService;
        }

        /*public IActionResult Index()
        {
            return View();
        }
       */

        [HttpPost]
        [Route("register-subregionrecord")]
        public async Task<IActionResult> Register(SubRegionRecordRequestModel requestModel)
        {

            try
            {
                var response = await _subRegionRecordService.Register(requestModel);
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
        [Route("subregion_records/{id}")]

        public async Task<IActionResult> Get(Guid id)
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
        [Route("sub-region_records")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _subRegionRecordService.GetAll();

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
        [Route("delete-subregion_record/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _subRegionRecordService.Delete(id);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 10, "white");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 10, "red");
            return Content(result.Message);
        }

        [HttpPost("update-subregion_record/{id}")]
        public async Task<IActionResult> Update(Guid id, SubRegionRecordRequestModel requestModel)
        {
            var result = await _subRegionRecordService.Update(id, requestModel);
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
