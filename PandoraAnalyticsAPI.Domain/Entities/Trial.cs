using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Domain.Entities
{
    public class Trial
    {
        public int Id { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }    

        public string Minigame { get; set; }
        public int Day { get; set; }
        public int TrialIndex { get; set; }

        public int Span { get; set; }
        public int WrongAttempts { get; set; }
        public int CompletionTimeMs { get; set; }

        public DateTime Timestamp { get; set; }

        public string TargetSequenceJson { get; set; }   

    }
}
