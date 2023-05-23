using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class QuestionRate
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey(nameof(ApiUser))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }
        [Required]
        public double Rate { get; set; }
        public DateTime Date_added { get; set; }

        [Required]
        [ForeignKey(nameof(Question))]
        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
