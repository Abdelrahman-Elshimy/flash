using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class CollectedAchievement
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public long AchievementId { get; set; }

    }
}
