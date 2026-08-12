using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using System.Text.Json;
namespace PandoraAnalyticsAPI.Infrastructure.Services
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GoogleSheetsService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SendLevelPlayAsync(
            LevelPlay levelPlay,
            string playerName)
        {
            var url =
                _configuration["GoogleSheets:WebhookUrl"];

            var secret =
                _configuration["GoogleSheets:Secret"];

            if (string.IsNullOrWhiteSpace(url) ||
                string.IsNullOrWhiteSpace(secret))
            {
                return false;
            }

            var payload = new
            {
                secret,
                eventId = levelPlay.EventId,
                playerId = levelPlay.PlayerId,
                playerName,
                minigame = levelPlay.Minigame,
                levelNumber = levelPlay.LevelNumber,
                successfulTrials = levelPlay.SuccessfulTrials,
                requiredTrials = levelPlay.RequiredTrials,
                normalPass = levelPlay.NormalPass,
                assistedPass = levelPlay.AssistedPass,
                activeDurationMs = levelPlay.ActiveDurationMs,
                startedAtUtc = levelPlay.StartedAtUtc.ToString("O"),
                completedAtUtc = levelPlay.CompletedAtUtc.ToString("O")
            };

            try
            {
     var response =
    await _httpClient.PostAsJsonAsync(url, payload);

if (!response.IsSuccessStatusCode)
    return false;

var responseBody =
    await response.Content.ReadAsStringAsync();

using var json =
    JsonDocument.Parse(responseBody);

return json.RootElement.TryGetProperty(
           "success",
           out var success)
       && success.GetBoolean();
            }
            catch
            {
                return false;
            }
        }
    }
}