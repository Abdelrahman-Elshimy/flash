using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class UserTriviaGifted
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

        [ForeignKey(nameof(TriviaGift))]
        public int TriviaGiftId { get; set; }
        public TriviaGift TriviaGift { get; set; }


        public bool Status { get; set; }

    }
}
