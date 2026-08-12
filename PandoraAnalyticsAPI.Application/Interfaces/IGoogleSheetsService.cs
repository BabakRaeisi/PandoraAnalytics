using PandoraAnalyticsAPI.Domain.Entities;

namespace PandoraAnalyticsAPI.Application.Interfaces
{
    public interface IGoogleSheetsService
    {
        Task<bool> SendLevelPlayAsync(
            LevelPlay levelPlay,
            string playerName
        );
    }
}