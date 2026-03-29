using PandoraAnalyticsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Application.Interfaces
{
    public interface ITrialRepository
    {
        Task AddRangeAsync(List<Trial> trials);
        Task<List<Trial>> GetAllAsync();
        Task<List<Trial>> GetBySessionIdAsync(int sessionId);
        Task<List<Trial>> GetByPlayerIdAsync(string playerId);
        Task<List<Trial>> GetByMinigameAsync(string minigame);
        Task<Trial?> GetByIdAsync(int trialId);
    }
}
