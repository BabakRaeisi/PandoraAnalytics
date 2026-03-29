using System;
using System.Collections.Generic;
using System.Text;

namespace PandoraAnalyticsAPI.Domain.Entities
{
    public  class Player
    {
        public string PlayerId { get; set; }  // stores phone number
        public string Name { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }
       
        public int AvatarIndex { get; set; }

        public List<Session> Sessions { get; set; } = new();
    }
}
