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
    public class MainProductController : ControllerBase
    {
        private readonly ImainProduct _mainProductService;

        public MainProductController(ImainProduct mainProductService)
        {
            _mainProductService = mainProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMainProducts()
        {
            var mainProducts = await _mainProductService.GetAll();
            return Ok(mainProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainProductById(string id)
        {
            var mainProduct = await _mainProductService.GetById(id);

            if (mainProduct == null)
            {
                return NotFound();
            }

            return Ok(mainProduct);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMainProduct([FromBody] MainProductModel mainProductModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _mainProductService.CreatebyModels(mainProductModel);
                return CreatedAtAction(nameof(GetMainProductById), new { id = mainProductModel.MainProductId }, mainProductModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainProduct(string id, [FromBody] MainProductModel mainProductModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mainProduct = await _mainProductService.GetById(id);

                if (mainProduct == null)
                {
                    return NotFound();
                }

                var updatedMainProduct = new MainProduct
                {
                    MainProductId = mainProductModel.MainProductId,
                    NameMp = mainProductModel.NameMp,
                    CategoriesId = mainProductModel.CategoriesId
                };

                await _mainProductService.Update(id, updatedMainProduct);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainProduct(string id)
        {
            var mainProduct = await _mainProductService.GetById(id);

            if (mainProduct == null)
            {
                return NotFound();
            }

            await _mainProductService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route("api/Mainproduct/createmodel")]

        public async Task<IActionResult> CreateMainproductModel([FromBody] MainProductModel mainProductModel)
        {
            try
            {
                await _mainProductService.CreatebyModels(mainProductModel);
                return Ok("MainP created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating MainP: {ex.Message}");
            }
        }
    }
}