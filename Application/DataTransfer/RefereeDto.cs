using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class RefereeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<int> LeaguesId{ get; set; }
    }
}
