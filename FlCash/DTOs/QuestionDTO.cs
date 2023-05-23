using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class BaseQuestionDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Rate { get; set; }
    }
    public class QuestionDTO: BaseQuestionDTO
    {
        public BaseLevelDTO Level { get; set; }
        public BaseCategoryDTO Category { get; set; }
        public virtual ICollection<BaseAnswerDTO> Answers { get; set; }
    }
    public class QuestionAnswersDTO : BaseQuestionDTO
    {
        public BaseLevelDTO Level { get; set; }
        public BaseCategoryDTO Category { get; set; }
        public virtual ICollection<BaseAnswerDTO> Answers { get; set; }
    }
    public class SuggestQuestionDTO
    {
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public long LevelId { get; set; }
        public String CorrectAnswer { get; set; }
        public String Answer2 { get; set; }
        public String Answer3 { get; set; }
        public String Answer4 { get; set; }
        public string UserId { get; set; }
    }
}
