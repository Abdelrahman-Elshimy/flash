using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Mission
    {
        public int Id { get; set; }
        public int QuestionCount { get; set; }
        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public int Coins { get; set; }
        public string Description { get; set; }
    }
}
