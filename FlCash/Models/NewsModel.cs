using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class NewsModel
    {
        public string Status { get; set; }
        public IList<New> News { get; set; }
    }
}
