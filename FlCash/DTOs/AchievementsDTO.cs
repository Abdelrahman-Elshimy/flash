using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class AchievementsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Coins { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public bool Status { get; set; }
        public double percentage { get; set; }
        public bool Collected { get; set; }
    }
    public class BaseAchievementsDTO
    {
        public string Logo { get; set; }
    }
}
