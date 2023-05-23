using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class OrderRsponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int OrderId { get; set; }
    }
}
