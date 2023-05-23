using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class QuestionResult
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Question))]
        public long QuestionId { get; set; }
        public Question Question { get; set; }

        [ForeignKey(nameof(Game))]
        public long GameId { get; set; }
        public Game Game { get; set; }

        public int CountOfAnswerOne { get; set; }
        public int CountOfAnswerTwo { get; set; }
        public int CountOfAnswerThree { get; set; }
        public int CountOfAnswerFour { get; set; }
    }
}
