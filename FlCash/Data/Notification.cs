﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
