using Microsoft.AspNetCore.Mvc;
using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produ_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierInfoController : ControllerBase
    {
        private readonly ISuppierInfo _supplierInfoService;

        public SupplierInfoController(ISuppierInfo supplierInfoService)
        {
            _supplierInfoService = supplierInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupplierInfo()
        {
            var supplierInfos = await _supplierInfoService.GetAll();
            return Ok(supplierInfos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierInfoById(string id)
        {
            var supplierInfo = await _supplierInfoService.GetById(id);

            if (supplierInfo == null)
            {
                return NotFound();
            }

            return Ok(supplierInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplierInfo([FromBody] SupplierInfoModel supplierInfoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _supplierInfoService.CreatebyModels(supplierInfoModel);
                return CreatedAtAction(nameof(GetSupplierInfoById), new { id = supplierInfoModel.SlId }, supplierInfoModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplierInfo(string id, [FromBody] SupplierInfoModel supplierInfoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supplierInfo = await _supplierInfoService.GetById(id);

                if (supplierInfo == null)
                {
                    return NotFound();
                }

                var updatedSupplierInfo = new SupplierInFo
                {
                    SlId = supplierInfoModel.SlId,
                    SupplierName = supplierInfoModel.SupplierName,
                    CategoriesId = supplierInfoModel.CategoriesId,
                    Address = supplierInfoModel.Address,
                    City = supplierInfoModel.City,
                    EstablishedYear = supplierInfoModel.EstablishedYear,
                    Numberofworkers = supplierInfoModel.Numberofworkers,
                    MainProductId = supplierInfoModel.MainProductId,
                    Moq = supplierInfoModel.Moq,
                    Certificate = supplierInfoModel.Certificate,
                    Customized = supplierInfoModel.Customized,
                    SampleProcess = supplierInfoModel.SampleProcess,
                    Leadtime = supplierInfoModel.Leadtime,
                    ExportUs = supplierInfoModel.ExportUs,
                    Websitelink = supplierInfoModel.Websitelink,
                    Email = supplierInfoModel.Email,
                    Phone = supplierInfoModel.Phone,
                    ContactPerson = supplierInfoModel.ContactPerson,
                    Note = supplierInfoModel.Note,
                    UserId = supplierInfoModel.UserId,
                    ReviewQa = supplierInfoModel.ReviewQa,
                    DateQa = supplierInfoModel.DateQa
                };

                await _supplierInfoService.Update(id, updatedSupplierInfo);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierInfo(string id)
        {
            var supplierInfo = await _supplierInfoService.GetById(id);

            if (supplierInfo == null)
            {
                return NotFound();
            }

            await _supplierInfoService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route("api/SuppierInfo/createmodel")]

        public async Task<IActionResult> CreateSuppilerModel([FromBody] SupplierInfoModel supplierInfo)
        {
            try
            {
                await _supplierInfoService.CreatebyModels(supplierInfo);
                return Ok("supplierInfo created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating supplierInfo: {ex.Message}");
            }
        }
    }
}