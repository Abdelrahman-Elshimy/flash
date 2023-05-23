using FlCash.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class RandomQuestions
    {
        public string Status { get; set; }
        public IList<QuestionAnswersDTO> Questions { get; set; }
    }
}
