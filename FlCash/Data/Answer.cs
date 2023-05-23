using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Answer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        [ForeignKey(nameof(Question))]
        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
