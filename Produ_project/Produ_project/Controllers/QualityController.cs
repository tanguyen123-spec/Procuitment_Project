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
    public class QualityController : ControllerBase
    {
        private readonly Iquality _qualityService;

        public QualityController(Iquality qualityService)
        {
            _qualityService = qualityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQualities()
        {
            var qualities = await _qualityService.GetAll();
            return Ok(qualities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQualityById(string id)
        {
            var quality = await _qualityService.GetById(id);

            if (quality == null)
            {
                return NotFound();
            }

            return Ok(quality);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuality([FromBody] QualityModel qualityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _qualityService.CreatebyModels(qualityModel);
                return CreatedAtAction(nameof(GetQualityById), new { id = qualityModel.Awid }, qualityModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuality(string id, [FromBody] QualityModel qualityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quality = await _qualityService.GetById(id);

                if (quality == null)
                {
                    return NotFound();
                }

                var updatedQuality = new Quality
                {
                    Awid = qualityModel.Awid,
                    Pcscustomer = qualityModel.Pcscustomer,
                    Color = qualityModel.Color,
                    Size = qualityModel.Size,
                    Note = qualityModel.Note
                };

                await _qualityService.Update(id, updatedQuality);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuality(string id)
        {
            var quality = await _qualityService.GetById(id);

            if (quality == null)
            {
                return NotFound();
            }

            await _qualityService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route("api/Quality/createmodel")]

        public async Task<IActionResult> CreateQualityModel([FromBody] QualityModel QualityModel)
        {
            try
            {
                await _qualityService.CreatebyModels(QualityModel);
                return Ok("Quality created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating Quality: {ex.Message}");
            }
        }
    }
}