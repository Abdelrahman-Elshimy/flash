using FlCash.Data;
using FlCash.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class AchievementModel
    {
        public string Status { get; set; }
        public IList<AchievementsDTO> achievements { get; set; }
    }
}
