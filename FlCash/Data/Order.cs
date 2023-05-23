using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

        public string OrderNumber { get; set; }

        public DateTime Date_Created { get; set; }

        [ForeignKey(nameof(StoreServicePlan))]
        public int StoreServicePlanId { get; set; }
        public StoreServicePlan StoreServicePlan { get; set; }
       
        public string Status { get; set; }

    }
}
