using Application.Practitioners;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionersController : ControllerBase
    {
        private readonly IPractitionerService _practitionerService;

        public PractitionersController(IPractitionerService practitionerService)
        {
            _practitionerService = practitionerService;
        }

        [HttpGet("by-specialty")]
        public async Task<IActionResult> GetBySpecialty([FromQuery] string code, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("Код специальности не может быть пустым.");
            }

            var practitioners = await _practitionerService.GetBySpecialtyAsync(code, ct);
            return Ok(practitioners);
        }
    }
}
