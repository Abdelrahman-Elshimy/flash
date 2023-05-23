using FlCash.Data;
using FlCash.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class CheckGameWithQuestionAndUsers
    {
        public long GameID { get; set; }

        public long CounterOfUsers { get; set; }

        public IList<BaseUs> Users { get; set; }

        public IList<QuestionAnswersDTO> GameQuestions { get; set; }
    }
}
