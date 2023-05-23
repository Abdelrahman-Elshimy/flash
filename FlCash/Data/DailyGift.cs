using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class DailyGift
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Value { get; set; }

        [ForeignKey(nameof(StoreService))]
        public int StoreServiceId { get; set; }
        public StoreService StoreService { get; set; }
    }
}
