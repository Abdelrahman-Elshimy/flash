using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlCash.Data;
using FlCash.Data.Configurations.Entities;

namespace FlCash.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}

        public DatabaseContext()
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<QuestionRate> QuestionRates { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<ModesIntro> ModesIntros { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Elmarka> Elmarkas { get; set; }
        public DbSet<ElmarkaTriesUser> ElmarkaTriesUsers { get; set; }
        public DbSet<ElmarkaGift> ElmarkaGifts { get; set; }
        public DbSet<RemainingTime> RemainingTimes { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Achieve> Achieves { get; set; }
        public DbSet<SuggestedQuestion> SuggestedQuestions { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<UserTriviaGifted> UserTriviaGifteds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<HwaratTrivia> hwaratTrivias { get; set; }
        public DbSet<HwaratTriviaMissionUser> HwaratTriviaMissionUsers { get; set; }
        public DbSet<DailyGift> DailyGifts { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Badge> Badges { get; set; }

       

        public DbSet<ModeDetail> ModeDetails { get; set; }
        public DbSet<CorrectUser> CorrectUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ModeDetailsConfiguration());

        }

        public DbSet<FlCash.Data.ModeDetail> ModeDetail { get; set; }

        public DbSet<FlCash.Data.Gift> Gift { get; set; }
        public DbSet<FlCash.Data.QuestionResult> QuestionResults { get; set; }
        public DbSet<FlCash.Data.StoreService> StoreServices { get; set; }
        public DbSet<FlCash.Data.StoreServicePlan> StoreServicePlans { get; set; }
        public DbSet<FlCash.Data.Offer> Offers { get; set; }
        public DbSet<CollectedAchievement> CollectedAchievements { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<FlCash.Data.TriviaGift> TriviaGift { get; set; }
        public DbSet<FlCash.Data.Trivia> Trivias { get; set; }

    }
}
