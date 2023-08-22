using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class TableSchemaController : Controller
    {

        private readonly ITableSchemaService _tableSchemaService;
        private readonly INotyfService _notifyService;
        public TableSchemaController(ITableSchemaService tableSchemaService, INotyfService notyfService)
        {
            _tableSchemaService = tableSchemaService;
            _notifyService = notyfService;
        }

        [HttpGet]
        [Route("register-tableschema")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TableSchemaRequestModel requestModel)
        {

            try
            {
                var response = await _tableSchemaService.Register(requestModel);
                _notifyService.Custom(response.Message, 40, "green");
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
        [Route("table_schemas/{id}")]

        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var response = await _tableSchemaService.Get(id);

                if (response.Status)
                {
                    _notifyService.Custom(response.Message, 40, "green");
                    return View(response.Data);
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
        [Route("table-schemas")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _tableSchemaService.GetAll();

                if (response.Status)
                {
                    _notifyService.Custom(response.Message, 40, "green");
                    return View(response.Data);
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

        [HttpPost]
        [Route("table_schemas/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _tableSchemaService.Delete(id);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 40, "green");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 40, "red");
            return Content(result.Message);
        }

        [HttpPost("update-table_schemas/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, TableSchemaRequestModel requestModel)
        {
            var result = await _tableSchemaService.Update(id, requestModel);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 40, "green");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 40, "red");
            Content(result.Message);
            return RedirectToAction();
        }
    }
}
