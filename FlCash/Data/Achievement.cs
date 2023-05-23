﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Achievement
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Coins { get; set; }
        public int NumberOfQuestion { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public bool Status { get; set; }

    }
}
