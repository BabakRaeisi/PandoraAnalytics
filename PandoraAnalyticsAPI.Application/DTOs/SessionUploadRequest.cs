using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Application.DTOs
{

    public record SessionUploadRequest(
        PlayerProfile profile,
        PlayerSaveData saveData,
        List<TrialRecord> trials
    );
    
    
    public record PlayerProfile(
    string phoneNumber,
    string playerName,
    int age,
    int avatarIndex,
    string gender
);

    public record PlayerSaveData(
        PlayerProfile profile,
        int currentDay,
        int miniGamesCompletedToday,
        int trialsCompletedInCurrentGame,
        bool bridgeCompletedToday,
        bool constellationCompletedToday,
        bool swmCompletedToday,
        bool programCompleted,
        bool profileCompleted,
        string lastDayCompletionTime
    );

    public record TrialRecord(
        string minigame_id,
        int day,
        int trial_index,
        int span,
        List<int> target_sequence,
        int wrong_attempts,
        int completion_time_ms,
        string timestamp_iso
    );

    public record SessionDetail(
        int sessionId,
        int day,
        DateTime completedAt,
        int miniGamesCompletedToday,
        int trialsCompletedInCurrentGame,
        bool bridgeCompletedToday,
        bool constellationCompletedToday,
        bool swmCompletedToday,
        bool programCompleted,
        bool profileCompleted,
        string lastDayCompletionTime,
        List<TrialRecord> trials
    );

    public record PlayerFullData(
        PlayerProfile profile,
        List<SessionDetail> sessions
    );

    public record ProfileRestoreResponse(
        bool isExistingPlayer,
        PlayerFullData playerData
    );
}
