using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class CollectCoins
    {
        public string UserId { get; set; }
        public int CountOfQuestions { get; set; }

        public int WrongAnswers { get; set; }
        //public List<long> QuestionAnswered { get; set; }
    }
}
