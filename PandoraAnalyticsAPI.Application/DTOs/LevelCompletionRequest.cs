namespace PandoraAnalyticsAPI.Application.DTOs
{
    public class LevelCompletionRequest
    {
        public string eventId { get; set; } = string.Empty;

        public string playerId { get; set; } = string.Empty;

        public string minigame { get; set; } = string.Empty;
        public int levelNumber { get; set; }

        public int successfulTrials { get; set; }
        public int requiredTrials { get; set; }

        public bool normalPass { get; set; }
        public bool assistedPass { get; set; }

        public int activeDurationMs { get; set; }

        public string startedAtUtc { get; set; } = string.Empty;
        public string completedAtUtc { get; set; } = string.Empty;
    }
}