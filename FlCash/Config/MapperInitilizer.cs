using AutoMapper;
using FlCash.DTOs;
using FlCash.Data;
using FlCash.Models;

namespace FlCash.Config
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {

            CreateMap<Category, GETCategoryDTO>().ReverseMap();
            CreateMap<Category, BaseCategoryDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, BaseQuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionAnswersDTO>().ReverseMap();
            CreateMap<Question, SuggestQuestionDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, BaseAnswerDTO>().ReverseMap();
            CreateMap<Level, BaseLevelDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<ApiUser, EditUserDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<ApiUser, BaseUserDTO>().ReverseMap();
            CreateMap<ApiUser, FrindUserDTO>().ReverseMap();
            CreateMap<ApiUser, UserStore>().ReverseMap();
            CreateMap<ApiUser, RegisterNewUser>().ReverseMap();
            CreateMap<Friend, FriendDTO>().ReverseMap();
            CreateMap<QuestionRate, QuestionRateDTO>().ReverseMap();
            CreateMap<QuestionRate, CreateQuestionRateDTO>().ReverseMap();
            CreateMap<ApiUser, UsersGame>().ReverseMap();
            CreateMap<ApiUser, BaseUs>().ReverseMap();
            CreateMap<QuestionResult, ResultQuestionDTO>().ReverseMap();
            CreateMap<StoreService, StoreServicesDTO>().ReverseMap();
            CreateMap<StoreServicePlan, StoreServicePlanDTO>().ReverseMap();
            CreateMap<Elmarka, ElmarkaDTO>().ReverseMap();
            CreateMap<ElmarkaGift, ElmarkaGiftDTO>().ReverseMap();
            CreateMap<ElmarkaTriesUser, ElmarkaTriesUserDTO>().ReverseMap();
            CreateMap<Achievement, BaseAchievementsDTO>().ReverseMap();
            
        }
    }
}
