using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class Club: BaseEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public int LeagueId { get; set; }

        public City City { get; set; }
        public League League { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
