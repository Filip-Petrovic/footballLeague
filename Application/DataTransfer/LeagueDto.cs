using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public IEnumerable<int> RefereesId { get; set; }
        public IEnumerable<int> ClubsId { get; set; }
    }
}
