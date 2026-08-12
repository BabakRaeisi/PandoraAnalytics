using PandoraAnalyticsAPI.Domain.Entities;

namespace PandoraAnalyticsAPI.Application.Interfaces
{
    public interface ILevelPlayRepository
    {
        Task<bool> ExistsByEventIdAsync(string eventId);

        Task AddAsync(LevelPlay levelPlay);

        Task<List<LevelPlay>> GetByPlayerIdAsync(string playerId);

        Task<List<LevelPlay>> GetAllAsync();
        Task<LevelPlay?> GetByEventIdAsync(string eventId);

Task MarkSheetSyncedAsync(LevelPlay levelPlay);
    }
}