using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using PandoraAnalyticsAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace PandoraAnalyticsAPI.Infrastructure.Repositories
{
    public class TrialRepository : ITrialRepository
    {
        private readonly AppDbContext _context;

        public TrialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Trial> trials)
        {
            _context.Trials.AddRange(trials);
                await _context.SaveChangesAsync();
        }

        public async Task<List<Trial>> GetAllAsync()
        {
            return await _context.Trials
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task<List<Trial>> GetBySessionIdAsync(int sessionId)
        {
            return await _context.Trials
                .AsNoTracking()
                .Where(t => t.SessionId == sessionId)
                .OrderBy(t => t.TrialIndex)
                .ToListAsync();
        }

        public async Task<List<Trial>> GetByPlayerIdAsync(string playerId)
        {
            return await _context.Trials
                .AsNoTracking()
                .Where(t => t.Session.PlayerId == playerId)
                .OrderBy(t => t.Day)
                .ThenBy(t => t.TrialIndex)
                .ToListAsync();
        }

        public async Task<List<Trial>> GetByMinigameAsync(string minigame)
        {
            return await _context.Trials
                .AsNoTracking()
                .Where(t => t.Minigame == minigame)
                .ToListAsync();
        }

        public async Task<Trial?> GetByIdAsync(int trialId)
        {
            return await _context.Trials
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == trialId);
        }
    }
}
