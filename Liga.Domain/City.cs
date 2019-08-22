using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public ICollection<Club> Clubs { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
