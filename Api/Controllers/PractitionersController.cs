using Application.Practitioners;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var practitioners = await _practitionerService.GetAllAsync(ct);
            return Ok(practitioners);
        }

        [HttpGet("specialties")]
        public async Task<IActionResult> GetAllSpecialties(CancellationToken ct)
        {
            var specialties = await _practitionerService.GetAllSpecialtiesAsync(ct);
            return Ok(specialties);
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
