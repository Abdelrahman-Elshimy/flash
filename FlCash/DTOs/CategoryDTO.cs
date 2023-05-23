using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class BaseCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class GETCategoryDTO: BaseCategoryDTO
    {
        public virtual ICollection<Question> Questions { get; set; }
    }
}
