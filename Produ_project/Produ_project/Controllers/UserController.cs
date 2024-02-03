using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Service;

namespace Produ_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var result = await _service.Getall();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);  
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _service.CreateByModels(user);
            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, User user)
        {
            if(string.IsNullOrEmpty(id) || user == null)
            {
                return BadRequest();
            }
            await _service.Update(id, user);
            return Ok(user);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return BadRequest(string.Empty);
            }
            await _service.Delete(id);
            return NoContent();
        }
    }
   
}
