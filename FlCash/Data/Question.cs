using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.Data
{
    [Index(nameof(Name), nameof(Rate))]
    public class Question
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public double Rate { get; set; }

        [ForeignKey(nameof(Level))]
        public long LevelId { get; set; }
        public Level Level { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public Category Category { get; set; }



        public virtual IList<Answer> Answers { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
