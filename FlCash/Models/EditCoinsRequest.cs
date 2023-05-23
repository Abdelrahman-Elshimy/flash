using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class EditCoinsRequest
    {
        public string Id { get; set; }
        public double Coins { get; set; }
    }
}
