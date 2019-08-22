using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class RefereeLeague
    {
        public int RefereeId { get; set; }
        public int LeagueId { get; set; }

        public Referee Referee { get; set; }
        public League League { get; set; }
    }
}
