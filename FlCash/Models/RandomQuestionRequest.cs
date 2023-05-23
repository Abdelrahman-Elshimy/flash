using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class RandomQuestionRequest
    {
        public List<long?> QuestionList { get; set; }
        public int PageSize { get; set; }
        public long? CategoryId { get; set; }
        public long? LevelId { get; set; }
    }
}
