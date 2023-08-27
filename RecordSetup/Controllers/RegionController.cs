using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class RegionController : Controller
    {

        private readonly IRegionService _regionService;
        private readonly INotyfService _notifyService;
        public RegionController(IRegionService regionService, INotyfService notyfService)
        {
            _regionService = regionService;
            _notifyService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register-region")]
        public async Task<IActionResult> Register(RegionRequestModel requestModel)
        {
            
            try
            {
                var response = await _regionService.Register(requestModel);
                _notifyService.Custom(response.Message, 10, "white");
                if (response.Status)
                {
                    return RedirectToAction("GetAll");
                }

                _notifyService.Custom(response.Message, 40, "red");
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

        [HttpGet]
        [Route("regions/{id}")]

        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var response = await _regionService.Get(id);

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
        [Route("regions")]
        [HttpGet]
        public IActionResult GetAll()
        { 
            return View();
        }

        [HttpPost]
        [Route("delete-region/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _regionService.Delete(id);
            if(result.Status)
            {
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 40, "red");
            return Content(result.Message);
        }
        
        [HttpPost("update-region/{id}")]
        public async Task<IActionResult> Update(Guid id, RegionRequestModel requestModel)
        {
            var result = await _regionService.Update(id, requestModel);
            if(result.Status)
            {
                _notifyService.Custom(result.Message, 40, "white");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 40, "red");
             Content(result.Message);
            return RedirectToAction();
        }
    }
}
