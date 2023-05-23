using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.Data
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual IList<Question> Questions { get; set; }
        public virtual IList<ElmarkaTriesUser> ElmarkaTriesUsers { get; set; }
    }
}
