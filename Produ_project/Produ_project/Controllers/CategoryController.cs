using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Service;

namespace Produ_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryService _categoryService;

        public CategoryController(IcategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "True")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.CreatebyModels(categoryModel);
                return CreatedAtAction(nameof(GetCategoryById), new { id = categoryModel.CategoriesId }, categoryModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = await _categoryService.GetById(id);

                if (category == null)
                {
                    return NotFound();
                }

                var updatedCategory = new Category
                {
                    CategoriesId = categoryModel.CategoriesId,
                    NameCategories = categoryModel.NameCategories
                };

                await _categoryService.Update(id, updatedCategory);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        [Route("api/Categories/createmodel")]

        public async Task<IActionResult> CreateCategoryModel([FromBody] CategoryModel categoryModel)
        {
            try
            {
                await _categoryService.CreatebyModels(categoryModel);
                return Ok("Category created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating Category: {ex.Message}");
            }
        }
    }
}
