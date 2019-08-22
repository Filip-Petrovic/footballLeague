using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class Player: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int CityId { get; set; }
        public int? ClubId { get; set; } 
        public int PositionId { get; set; }
        public City City { get; set; }
        public Club Club { get; set; }
        public Position Position{ get; set; }

    }
}
