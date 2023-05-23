using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class StoreService
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }

        public virtual IList<StoreServicePlan> StoreServicePlans { get; set; }

    }
}
