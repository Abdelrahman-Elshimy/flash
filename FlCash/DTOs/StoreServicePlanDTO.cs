﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class StoreServicePlanDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
