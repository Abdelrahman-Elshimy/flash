using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Answer> _answers;
        private IGenericRepository<Game> _games;
        private IGenericRepository<Level> _levels;
        private IGenericRepository<Question> _questions;
        private IGenericRepository<ApiUser> _apiusers;
        private IGenericRepository<New> _news;
        private IGenericRepository<Mode> _modes;
        private IGenericRepository<ModesIntro> _modesIntros;
        private IGenericRepository<Gift> _gifts;
        private IGenericRepository<StoreServicePlan> _storeServicePlan;
        private IGenericRepository<StoreService> _storeService;
        private IGenericRepository<Elmarka> _elmarkas;
        private IGenericRepository<ElmarkaTriesUser> _elmarkaTriesUser;
        private IGenericRepository<CollectedAchievement> _collectedachievements;
        private IGenericRepository<Achievement> _achievements;
        

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
        public IGenericRepository<Answer> Answers => _answers ??= new GenericRepository<Answer>(_context);
        public IGenericRepository<Game> Games => _games ??= new GenericRepository<Game>(_context);
        public IGenericRepository<Level> Levels => _levels ??= new GenericRepository<Level>(_context);
        public IGenericRepository<Question> Questions => _questions ??= new GenericRepository<Question>(_context);

        public IGenericRepository<ApiUser> ApiUsers => _apiusers ??= new GenericRepository<ApiUser>(_context);
        public IGenericRepository<New> News => _news ??= new GenericRepository<New>(_context);
        public IGenericRepository<Mode> Modes => _modes ??= new GenericRepository<Mode>(_context);
        public IGenericRepository<ModesIntro> ModesIntros => _modesIntros ??= new GenericRepository<ModesIntro>(_context);
        public IGenericRepository<Gift> Gifts => _gifts ??= new GenericRepository<Gift>(_context);
        public IGenericRepository<StoreServicePlan> StoreServicePlans => _storeServicePlan ??= new GenericRepository<StoreServicePlan>(_context);
        public IGenericRepository<StoreService> StoreServices => _storeService ??= new GenericRepository<StoreService>(_context);
        public IGenericRepository<ElmarkaTriesUser> ElmarkaTriesUsers => _elmarkaTriesUser ??= new GenericRepository<ElmarkaTriesUser>(_context);

        public IGenericRepository<Elmarka> Elmarkas => _elmarkas ??= new GenericRepository<Elmarka>(_context);
        public IGenericRepository<CollectedAchievement> CollectedAchievements => _collectedachievements ??= new GenericRepository<CollectedAchievement>(_context);
        public IGenericRepository<Achievement> Achievements => _achievements ??= new GenericRepository<Achievement>(_context);

        public void Dispose()
        {

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}
