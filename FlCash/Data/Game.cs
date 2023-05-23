using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Game
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Mode))]
        public long ModeId { get; set; }
        public Mode Mode { get; set; }

        public long Counter { get; set; }
        public  bool Status { get; set; }

        public virtual List<Question> Questions { get; set; }
        public virtual List<ApiUser> Users { get; set; }
        public virtual List<CorrectUser> CorrectUsers { get; set; }
        public virtual List<QuestionResult> QuestionResults { get; set; }




    }
}
