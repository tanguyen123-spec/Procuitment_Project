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
    public class ArtWorkController : ControllerBase
    {
        private readonly IArtWorkService _artWorkService;

        public ArtWorkController(IArtWorkService artWorkService)
        {
            _artWorkService = artWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtWorks()
        {
            var artWorks = await _artWorkService.GetAll();
            return Ok(artWorks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtWorkById(string id)
        {
            var artWork = await _artWorkService.GetById(id);

            if (artWork == null)
            {
                return NotFound();
            }

            return Ok(artWork);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtWork([FromBody] ArtWorkModel artWorkModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artWorkService.CreatebyModels(artWorkModel);
                return CreatedAtAction(nameof(GetArtWorkById), new { id = artWorkModel.Awid }, artWorkModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtWork(string id, [FromBody] ArtWorkModel artWorkModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var artWork = await _artWorkService.GetById(id);

                if (artWork == null)
                {
                    return NotFound();
                }

                var updatedArtWork = new ArtWork
                {
                    Awid = artWorkModel.Awid,
                    NameAw = artWorkModel.NameAw,
                    MainProductId = artWorkModel.MainProductId,
                    ImgagesUrl = artWorkModel.ImgagesUrl
                };

                await _artWorkService.Update(id, updatedArtWork);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtWork(string id)
        {
            var artWork = await _artWorkService.GetById(id);

            if (artWork == null)
            {
                return NotFound();
            }

            await _artWorkService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route("api/artwork/createModel")]
        public async Task<IActionResult> CreateArtWorkModel([FromBody] ArtWorkModel artWorkModel)
        {
            try
            {
                await _artWorkService.CreatebyModels(artWorkModel);
                return Ok("ArtWork created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating ArtWork: {ex.Message}");
            }
        }

    }
}