using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class HwaratTriviaMissionUser
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

        [ForeignKey(nameof(Mission))]
        public int MissionId { get; set; }
        public Mission Mission { get; set; }

        public int CountOfQuestionsSolved { get; set; }
    }
}
