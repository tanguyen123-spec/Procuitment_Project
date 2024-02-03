using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produ_project.Secure.Models;
using Produ_project.Secure.Service;

namespace Produ_project.Secure.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenServices _services;
        public TokenController(ITokenServices services)
        {
            _services = services;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(LoginModels models)
        {
            var result = await _services.Validate(models);
            if (result == null)
            {
                return BadRequest("đã có lỗi!");
            }
            return Ok(result);
        }

        [HttpPost("renew")]
        public async Task<IActionResult> RenewToken(TokenModels models)
        {
            var result = await _services.RenewToken(models);
            if (result == null)
            {
                return BadRequest("đã có lỗi renew!");
            }
            return Ok(result);
        }
    }
}
