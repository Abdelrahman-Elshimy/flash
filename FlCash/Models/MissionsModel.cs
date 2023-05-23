using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class MissionsModel
    {
        public int Id { get; set; }
        public int QuestionCount { get; set; }
        public long CategoryId { get; set; }
        public int Coins { get; set; }
        public string Description { get; set; }
    }
}
