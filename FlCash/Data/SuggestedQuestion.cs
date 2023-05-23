using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class SuggestedQuestion
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public long QuestionId { get; set; }
    }
}
