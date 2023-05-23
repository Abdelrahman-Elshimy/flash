using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class ElmarkaDTO
    {

        public string RemainDate { get; set; }

        public int Keys { get; set; }

        public decimal Percentage { get; set; }

        public virtual List<ElmarkaTriesUserDTO> Tries { get; set; }

        
    }
}
