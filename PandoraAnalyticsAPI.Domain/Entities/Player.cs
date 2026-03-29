using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Domain.Entities
{
    public  class Player
    {
        public string PlayerId { get; set; } = string.Empty;  // stores phone number
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;
       
        public int AvatarIndex { get; set; }

        public List<Session> Sessions { get; set; } = new();
    }
}
