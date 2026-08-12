using Microsoft.EntityFrameworkCore;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using PandoraAnalyticsAPI.Infrastructure.Data;

namespace PandoraAnalyticsAPI.Infrastructure.Repositories
{
    public class LevelPlayRepository : ILevelPlayRepository
    {
        private readonly AppDbContext _context;

        public LevelPlayRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEventIdAsync(string eventId)
        {
            return await _context.LevelPlays
                .AnyAsync(x => x.EventId == eventId);
        }

        public async Task AddAsync(LevelPlay levelPlay)
        {
            _context.LevelPlays.Add(levelPlay);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LevelPlay>> GetByPlayerIdAsync(string playerId)
        {
            return await _context.LevelPlays
                .Where(x => x.PlayerId == playerId)
                .OrderBy(x => x.CompletedAtUtc)
                .ToListAsync();
        }

        public async Task<List<LevelPlay>> GetAllAsync()
        {
            return await _context.LevelPlays
                .OrderBy(x => x.CompletedAtUtc)
                .ToListAsync();
        }

   public async Task<LevelPlay?> GetByEventIdAsync(string eventId)
{
    return await _context.LevelPlays
        .FirstOrDefaultAsync(x => x.EventId == eventId);
}

public async Task MarkSheetSyncedAsync(LevelPlay levelPlay)
{
    levelPlay.SheetSynced = true;
    await _context.SaveChangesAsync();
}
    }
}