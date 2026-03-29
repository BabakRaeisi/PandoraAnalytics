using PandoraAnalyticsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Application.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllAsync();
        Task<Player?> GetByIdAsync(string playerId);
        Task<Player?> GetByIdWithSessionsAndTrialsAsync(string playerId);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task<List<Player>> GetAllWithSessionsAndTrialsAsync();
    }
}
