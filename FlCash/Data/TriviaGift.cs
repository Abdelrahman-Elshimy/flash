using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class TriviaGift
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public int CorrectAnswers { get; set; }
        public int hearts { get; set; }
        public int Coins { get; set; }
        public int Tickets { get; set; }
    }
}
