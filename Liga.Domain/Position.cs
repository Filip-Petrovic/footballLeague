﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Liga.Domain
{
    public class Position: BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
