using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class Referee: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<RefereeLeague> RefereeLeagues { get; set; }
    }
}
