using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class ModesIntro
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey(nameof(Mode))]
        public long ModeId { get; set; }
        public Mode Mode { get; set; }
    }
}
