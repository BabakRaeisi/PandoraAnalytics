using Microsoft.EntityFrameworkCore;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using PandoraAnalyticsAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

      public async Task<List<Player>> GetAllAsync()
{
    return await _context.Players.ToListAsync();
}

public async Task<Player?> GetByIdAsync(string playerId)
{
    return await _context.Players
        .FirstOrDefaultAsync(p => p.PlayerId == playerId);
}

        public async Task AddAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Player>> GetAllWithSessionsAndTrialsAsync()
        {
            return await _context.Players
                .Include(p => p.Sessions)
                .ThenInclude(s => s.Trials)
                .ToListAsync();
        }
    }
}
