using PandoraAnalyticsAPI.Application.DTOs;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Domain.Entities;
using System.Text.Json;

namespace PandoraAnalyticsAPI.Application.Services
{
    public class AnalyticsService
    {
        private readonly IPlayerRepository _playerRepo;
        private readonly ISessionRepository _sessionRepo;
        private readonly ITrialRepository _trialRepo;

        public AnalyticsService(
            IPlayerRepository playerRepo,
            ISessionRepository sessionRepo,
            ITrialRepository trialRepo)
        {
            _playerRepo = playerRepo;
            _sessionRepo = sessionRepo;
            _trialRepo = trialRepo;
        }

        private static PlayerProfile MapPlayerProfile(Player player)
        {
            return new PlayerProfile(
                player.PlayerId,
                player.Name,
                player.Age,
                player.AvatarIndex,
                player.Gender
            );
        }

        private static TrialRecord MapTrialRecord(Trial trial)
        {
            return new TrialRecord(
                trial.Minigame,
                trial.Day,
                trial.TrialIndex,
                trial.Span,
                JsonSerializer.Deserialize<List<int>>(trial.TargetSequenceJson) ?? new(),
                trial.WrongAttempts,
                trial.CompletionTimeMs,
                trial.Timestamp.ToString("o")
            );
        }

        private static SessionDetail MapSessionDetail(Session session)
        {
            return new SessionDetail(
                session.Id,
                session.Day,
                session.CompletedAt,
                session.MiniGamesCompletedToday,
                session.TrialsCompletedInCurrentGame,
                session.BridgeCompletedToday,
                session.ConstellationCompletedToday,
                session.SwmCompletedToday,
                session.ProgramCompleted,
                session.ProfileCompleted,
                session.LastDayCompletionTime,
                session.Trials.Select(MapTrialRecord).ToList()
            );
        }

        private static PlayerFullData MapPlayerFullData(Player player)
        {
            return new PlayerFullData(
                MapPlayerProfile(player),
                player.Sessions
                    .OrderBy(s => s.Id)
                    .Select(MapSessionDetail)
                    .ToList()
            );
        }

        public async Task<ProfileRestoreResponse> CreateOrRestoreProfile(PlayerProfile profile)
        {
            var existingPlayer = await _playerRepo.GetByIdAsync(profile.phoneNumber);

            if (existingPlayer == null)
            {
                var player = new Player
                {
                    PlayerId = profile.phoneNumber,
                    Name = profile.playerName,
                    Age = profile.age,
                    Gender = profile.gender,
                    AvatarIndex = profile.avatarIndex
                };

                await _playerRepo.AddAsync(player);

                return new ProfileRestoreResponse(false, MapPlayerFullData(player));
            }

            existingPlayer.Name = profile.playerName;
            existingPlayer.Age = profile.age;
            existingPlayer.Gender = profile.gender;
            existingPlayer.AvatarIndex = profile.avatarIndex;

            await _playerRepo.UpdateAsync(existingPlayer);

            var fullPlayer = (await _playerRepo.GetAllWithSessionsAndTrialsAsync())
                .First(p => p.PlayerId == profile.phoneNumber);

            return new ProfileRestoreResponse(true, MapPlayerFullData(fullPlayer));
        }

        // ---------------- UPLOAD ----------------
        public async Task HandleUpload(SessionUploadRequest request)
        {
            var existingPlayer = await _playerRepo.GetByIdAsync(request.profile.phoneNumber);

            Player player;

            if (existingPlayer == null)
            {
                player = new Player
                {
                    PlayerId = request.profile.phoneNumber,
                    Name = request.profile.playerName,
                    Age = request.profile.age,
                    Gender = request.profile.gender,
                    AvatarIndex = request.profile.avatarIndex
                };

                await _playerRepo.AddAsync(player);
            }
            else
            {
                player = existingPlayer;
                player.Name = request.profile.playerName;
                player.Age = request.profile.age;
                player.Gender = request.profile.gender;
                player.AvatarIndex = request.profile.avatarIndex;

                await _playerRepo.UpdateAsync(player);
            }

            var session = new Session
            {
                PlayerId = player.PlayerId,
                Day = request.saveData.currentDay,
                CompletedAt = DateTime.UtcNow,

                MiniGamesCompletedToday = request.saveData.miniGamesCompletedToday,
                TrialsCompletedInCurrentGame = request.saveData.trialsCompletedInCurrentGame,

                BridgeCompletedToday = request.saveData.bridgeCompletedToday,
                ConstellationCompletedToday = request.saveData.constellationCompletedToday,
                SwmCompletedToday = request.saveData.swmCompletedToday,

                ProgramCompleted = request.saveData.programCompleted,
                ProfileCompleted = request.saveData.profileCompleted,

                LastDayCompletionTime = request.saveData.lastDayCompletionTime
            };

            session = await _sessionRepo.AddAsync(session);

            var trials = request.trials.Select(t => new Trial
            {
                SessionId = session.Id,
                Minigame = t.minigame_id,
                Day = t.day,
                TrialIndex = t.trial_index,
                Span = t.span,
                WrongAttempts = t.wrong_attempts,
                CompletionTimeMs = t.completion_time_ms,
                Timestamp = DateTime.Parse(t.timestamp_iso).ToUniversalTime(),
                TargetSequenceJson = JsonSerializer.Serialize(t.target_sequence)
            }).ToList();

            await _trialRepo.AddRangeAsync(trials);
        }

        // ---------------- READ ----------------
        public async Task<List<PlayerProfile>> GetPlayers()
        {
            var players = await _playerRepo.GetAllAsync();

            return players.Select(MapPlayerProfile).ToList();
        }

        public async Task<List<PlayerSaveData>> GetPlayerSessions(string playerId)
        {
            var sessions = await _sessionRepo.GetByPlayerIdAsync(playerId);

            return sessions.Select(s => new PlayerSaveData(
                new PlayerProfile(
                    s.Player.PlayerId,
                    s.Player.Name,
                    s.Player.Age,
                    s.Player.AvatarIndex,
                    s.Player.Gender
                ),
                s.Day,
                s.MiniGamesCompletedToday,
                s.TrialsCompletedInCurrentGame,
                s.BridgeCompletedToday,
                s.ConstellationCompletedToday,
                s.SwmCompletedToday,
                s.ProgramCompleted,
                s.ProfileCompleted,
                s.LastDayCompletionTime
            )).ToList();
        }

        public async Task<List<TrialRecord>> GetPlayerTrials(string playerId)
        {
            var trials = await _trialRepo.GetByPlayerIdAsync(playerId);

            return trials.Select(MapTrialRecord).ToList();
        }

        public async Task<List<TrialRecord>> GetAllTrials()
        {
            var trials = await _trialRepo.GetAllAsync();

            return trials.Select(MapTrialRecord).ToList();
        }

        public async Task<List<TrialRecord>> GetSessionTrials(int sessionId)
        {
            var trials = await _trialRepo.GetBySessionIdAsync(sessionId);

            return trials.Select(MapTrialRecord).ToList();
        }

        public async Task<List<PlayerFullData>> GetAllPlayersWithData()
        {
            var players = await _playerRepo.GetAllWithSessionsAndTrialsAsync();

            return players.Select(MapPlayerFullData).ToList();
        }
    }
}