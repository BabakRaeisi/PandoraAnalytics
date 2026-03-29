using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Domain.Entities
{
    public  class Session
    {

        public int Id { get; set; }

        public string PlayerId { get; set; }
        public Player Player { get; set; }    

        public int Day { get; set; }
        public DateTime CompletedAt { get; set; }

        public int MiniGamesCompletedToday { get; set; }
        public int TrialsCompletedInCurrentGame { get; set; }

        public bool BridgeCompletedToday { get; set; }
        public bool ConstellationCompletedToday { get; set; }
        public bool SwmCompletedToday { get; set; }

        public bool ProgramCompleted { get; set; }
        public bool ProfileCompleted { get; set; }

        public string LastDayCompletionTime { get; set; }

        public List<Trial> Trials { get; set; } = new();

    }
}
