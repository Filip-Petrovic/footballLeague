using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public IEnumerable<int> ClubsId { get; set; }
        public IEnumerable<int> PlayersId { get; set; }
    }
}
