using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<int> PlayersId { get; set; }
    }
}
