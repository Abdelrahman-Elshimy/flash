using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class ModeDetailsModel
    {
        public string RemainingDays { get; set; }
        public List<Gift> Gifts { get; set; }

    }

}
