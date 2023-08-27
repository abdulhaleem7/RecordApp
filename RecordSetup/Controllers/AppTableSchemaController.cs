using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class AppTableSchemaController : Controller
    {

        private readonly ISubRegionRecordTableService _subRegionRecordTableService;
        private readonly ITableSchemaService _tableSchemaService;
        private readonly INotyfService _notifyService;
        public AppTableSchemaController(ITableSchemaService tableSchemaService, 
                                        INotyfService notyfService,
                                        ISubRegionRecordTableService subRegionRecordTableService)
        {
            _tableSchemaService = tableSchemaService;
            _notifyService = notyfService;
            _subRegionRecordTableService = subRegionRecordTableService;
        }

        [HttpPost]
        [Route("register-tableschema")]
        public async Task<IActionResult> Register(TableSchemaRequestModel requestModel)
        {

            try
            {
                var response = await _tableSchemaService.Register(requestModel);
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
        [Route("table_schemas/{id}")]

        public async Task<IActionResult> Get( Guid id)
        {
            try
            {
                var response = await _tableSchemaService.Get(id);

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
        [Route("app_table_schemas")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _tableSchemaService.GetAll();

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
        [Route("Table-details/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllTableSchemaAsyncByTableId(Guid id)
        {
            try
            {
                var response = await _subRegionRecordTableService.Get(id);

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
        [Route("delete-table_schemas/{id}")]
        public async Task<IActionResult> Delete( Guid id)
        {
            var result = await _tableSchemaService.Delete(id);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 10, "white");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 10, "red");
            return Content(result.Message);
        }

        [HttpPost("update-table_schemas/{id}")]
        public async Task<IActionResult> Update( Guid id, TableSchemaRequestModel requestModel)
        {
            var result = await _tableSchemaService.Update(id, requestModel);
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
