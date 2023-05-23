using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Elmarka
    {
        public long Id { get; set; }

        public int Keys { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

        public virtual List<ElmarkaTriesUser> ElmarkaTriesUsers { get; set; }


    }
}
