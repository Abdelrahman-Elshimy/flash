using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Friend
    {
        public long Id { get; set; }

        public string FaceBookId { get; set; }

        public bool Status { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

    }
}
