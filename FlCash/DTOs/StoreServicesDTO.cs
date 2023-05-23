using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class StoreServicesDTO
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }

        public virtual IList<StoreServicePlanDTO> StoreServicePlans { get; set; }
    }
}
