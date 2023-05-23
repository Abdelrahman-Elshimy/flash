using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Mode
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual IList<ModesIntro> ModesIntros { get; set; }
    }
}
