using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class ElmarkaTriesUser
    {
        public long Id { get; set; }

        public int Index { get; set; }

        [ForeignKey(nameof(ElmarkaGift))]
        public long ElmarkaGiftId { get; set; }
        public ElmarkaGift ElmarkaGift { get; set; }

        [ForeignKey(nameof(Elmarka))]
        public long ElmarkaId { get; set; }
        public Elmarka Elmarka { get; set; }


        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public int Status { get; set; }


        public int Tries { get; set; }
    }
}
