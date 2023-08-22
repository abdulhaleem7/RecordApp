﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RecordSetup.Dto;
using RecordSetup.Interface.Servicies;

namespace RecordSetup.Controllers
{
    public class SubRegionRecordTableController : Controller
    {

        private readonly ISubRegionRecordTableService _subRegionRecordTableService;
        private readonly INotyfService _notifyService;
        public SubRegionRecordTableController(ISubRegionRecordTableService subRegionRecordTableService, INotyfService notyfService)
        {
            _subRegionRecordTableService = subRegionRecordTableService;
            _notifyService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("register-subregionrecord_table")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SubRegionRecordTableRequestModel requestModel)
        {

            try
            {
                var response = await _subRegionRecordTableService.Register(requestModel);
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
        [Route("sub-region_record_tables/{id}")]

        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var response = await _subRegionRecordTableService.Get(id);

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
        [Route("sub-region_record_tables")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _subRegionRecordTableService.GetAll();

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
        [Route("delete-sub-region_record_tables/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _subRegionRecordTableService.Delete(id);
            if (result.Status)
            {
                _notifyService.Custom(result.Message, 40, "green");
                return RedirectToAction("GetAll");
            }
            _notifyService.Custom(result.Message, 40, "red");
            return Content(result.Message);
        }

        [HttpPost("update-subregion_record_tables/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, SubRegionRecordTableRequestModel requestModel)
        {
            var result = await _subRegionRecordTableService.Update(id, requestModel);
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