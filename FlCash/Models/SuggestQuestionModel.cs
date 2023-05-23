using FlCash.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Models
{
    public class SuggestQuestionModel
    {
        public List<BaseCategoryDTO> Categories { get; set; }
        public List<BaseLevelDTO> Levels { get; set; }
    }
}
