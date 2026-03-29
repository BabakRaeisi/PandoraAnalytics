using Microsoft.EntityFrameworkCore;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using PandoraAnalyticsAPI.Infrastructure.Data;

namespace PandoraAnalyticsAPI.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session> AddAsync(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();   // MUST be here

        return session; // ID is generated here
    }

    public async Task<List<Session>> GetByPlayerIdAsync(string playerId)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Player) // REQUIRED
            .Where(s => s.PlayerId == playerId)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int sessionId)
    {
        return await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }
}