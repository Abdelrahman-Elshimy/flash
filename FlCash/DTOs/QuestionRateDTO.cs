using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class CreateQuestionRateDTO
    {
        

        public string UserId { get; set; }

        public long QuestionId { get; set; }

        public double Rate { get; set; }
    }
    public class QuestionRateDTO : CreateQuestionRateDTO
    {

    }
}
