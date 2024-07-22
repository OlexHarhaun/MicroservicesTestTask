using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProjectService.Data;
using ProjectService.Interfaces;
using ProjectService.Responses;
using System.Text.Json;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PopularIndicatorsController : ControllerBase
    {
        private readonly IPopularIndicatorsService _popularIndicatorsService;
        private readonly ILogger<PopularIndicatorsController> _logger;

        public PopularIndicatorsController(IPopularIndicatorsService popularIndicatorsService, ILogger<PopularIndicatorsController> logger)
        {
            _popularIndicatorsService = popularIndicatorsService;
            _logger = logger;
        }

        [HttpGet("popularIndicators/{subscriptionType}")]
        public async Task<ActionResult<PopularIndicatorsResponse>> GetPopularIndicators(string subscriptionType)
        {
            try
            {
                var result = await _popularIndicatorsService.GetPopularIndicatorsAsync(subscriptionType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving popular indicators.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
