using PandoraAnalyticsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Application.Interfaces
{
    public interface ISessionRepository
    {
        Task<Session> AddAsync(Session session);
        Task<List<Session>> GetByPlayerIdAsync(string playerId);
        Task<Session?> GetByIdAsync(int sessionId);
    }
}
