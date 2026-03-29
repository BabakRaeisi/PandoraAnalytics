using Microsoft.AspNetCore.Mvc;
using PandoraAnalyticsAPI.Application.DTOs;
using PandoraAnalyticsAPI.Application.Services;

namespace PandoraAnalyticsAPI.API.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AnalyticsService _service;

        public AnalyticsController(AnalyticsService service)
        {
            _service = service;
        }

        // -------- UPLOAD --------
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] SessionUploadRequest request)
        {
            await _service.HandleUpload(request);
            return Ok();
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateOrRestoreProfile([FromBody] PlayerProfile profile)
        {
            return Ok(await _service.CreateOrRestoreProfile(profile));
        }

        // -------- READ --------
        [HttpGet("players")]
        public async Task<IActionResult> GetPlayers()
        {
            return Ok(await _service.GetPlayers());
        }

        [HttpGet("players/{phoneNumber}/sessions")]
        public async Task<IActionResult> GetPlayerSessions(string phoneNumber)
        {
            return Ok(await _service.GetPlayerSessions(phoneNumber));
        }

        [HttpGet("players/{phoneNumber}/trials")]
        public async Task<IActionResult> GetPlayerTrials(string phoneNumber)
        {
            return Ok(await _service.GetPlayerTrials(phoneNumber));
        }

        [HttpGet("trials")]
        public async Task<IActionResult> GetAllTrials()
        {
            return Ok(await _service.GetAllTrials());
        }

        [HttpGet("sessions/{sessionId}/trials")]
        public async Task<IActionResult> GetSessionTrials(int sessionId)
        {
            return Ok(await _service.GetSessionTrials(sessionId));
        }
    }
}