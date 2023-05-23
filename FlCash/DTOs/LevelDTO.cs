using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class BaseLevelDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class LevelDTO: BaseLevelDTO
    {
        public virtual IList<BaseQuestionDTO> Questions { get; set; }
    }
}
