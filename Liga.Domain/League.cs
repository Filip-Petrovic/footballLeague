using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class League: BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public ICollection<RefereeLeague> LeagueReferees { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
