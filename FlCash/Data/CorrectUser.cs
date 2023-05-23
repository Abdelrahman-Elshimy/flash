using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class CorrectUser
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Game))]
        public long GameId { get; set; }
        public Game Game { get; set; }

        [ForeignKey(nameof(ApiUser))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

    }
}
