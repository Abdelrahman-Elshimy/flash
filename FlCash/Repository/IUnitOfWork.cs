using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Answer> Answers { get; }
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Level> Levels { get; }
        IGenericRepository<Question> Questions { get; }
        IGenericRepository<ApiUser> ApiUsers { get; }
        IGenericRepository<New> News { get; }
        IGenericRepository<Mode> Modes { get; }
        IGenericRepository<ModesIntro> ModesIntros { get; }
        IGenericRepository<Gift> Gifts { get; }
        IGenericRepository<StoreService> StoreServices { get; }
        IGenericRepository<StoreServicePlan> StoreServicePlans { get; }
        IGenericRepository<Elmarka> Elmarkas { get; }
        IGenericRepository<ElmarkaTriesUser> ElmarkaTriesUsers { get; }
        IGenericRepository<CollectedAchievement> CollectedAchievements { get; }
        IGenericRepository<Achievement> Achievements { get; }
        


        Task Save();
        
    }
}
