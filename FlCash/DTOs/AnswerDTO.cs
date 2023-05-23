using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class BaseAnswerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }
    public class AnswerDTO: BaseAnswerDTO
    {
        public BaseQuestionDTO Question { get; set; }
    }
}
