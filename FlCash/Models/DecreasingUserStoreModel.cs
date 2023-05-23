using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class DecreasingUserStoreModel
    {
        public string UserId { get; set; }
        public int Coins { get; set; }
        public int Hearts { get; set; }
        public int Tickets { get; set; }
        public int CorrectAnswers { get; set; }
    }
    public class UpdateUserStore
    {
        public string UserId { get; set; }
        public int Coins { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
