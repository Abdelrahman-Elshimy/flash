using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class StoreServicePlan
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [ForeignKey(nameof(StoreService))]
        public int StoreServiceId { get; set; }
        public StoreService StoreService { get; set; }
       


    }
}
