using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int LeagueId { get; set; }
        public IEnumerable<int> PlayersId { get; set; }
    }
}
