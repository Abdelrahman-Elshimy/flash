using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class TriviaGiftsModel
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Gift { get; set; }
    }

    public class TriviaModel
    {
        public String Data { get; set; }
        public List<TriviaGiftsModel> Gifts { get; set; }
        public bool Status { get; set; }
    }
}
