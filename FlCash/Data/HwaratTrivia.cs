using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class HwaratTrivia
    {
        public int Id { get; set; }
        public int MissionsCompleted { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }
    }
}
