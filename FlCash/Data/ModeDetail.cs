using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class ModeDetail
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [ForeignKey(nameof(Mode))]
        public long ModeId { get; set; }
        public Mode Mode { get; set; }

    }
}
