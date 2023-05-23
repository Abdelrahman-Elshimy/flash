using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class OrderModel
    {

        public string UserId { get; set; }


        public int StoreServicePlanId { get; set; }
    }
    public class ActivateOrderId
    {
        public int OrderId { get; set; }
    }
}
