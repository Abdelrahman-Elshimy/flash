using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class ElmarkaTriesUserDTO
    {
        public int Index { get; set; }

        //public ElmarkaGiftDTO ElmarkaGift { get; set; }

        public long CategoryId { get; set; }

        public int Status { get; set; }

        public int Tries { get; set; }
    }
}
