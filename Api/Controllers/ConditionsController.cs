using Application.Conditions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionsController : ControllerBase
    {
        private readonly IConditionService _conditionService;

        public ConditionsController(IConditionService conditionService)
        {
            _conditionService = conditionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var conditions = await _conditionService.GetAllAsync(ct);
            return Ok(conditions);
        }
    }
}
