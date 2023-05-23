using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class UserIdModel
    {
        public string userId { get; set; }
    }
    public class UserTriviaModelID
    {
        public string userId { get; set; }
        public int? GiftId { get; set; }
    }
    public class CollectDailyGift
    {
        public string userId { get; set; }
        public int? GiftId { get; set; }
    }
}
