using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FlCash.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Image { get; set; }
        public string FaceId { get; set; }
        public double Coins { get; set; }
        public double CoinsPerWeek { get; set; }
        public int CorrectAnswers { get; set; }
        public int Hearts { get; set; }
        public int Tickets { get; set; }
        public int WiningGameCounter { get; set; }
        public int WiningQuestionCounter { get; set; }
        public DateTime LastEnter { get; set; }
        public int CounterDailyGift { get; set; }

        public virtual IList<Friend> Friends { get; set; }
        public virtual List<Game> Games { get; set; }

        public int EasyLevelQuestionCount { get; set; }
        public int MediumLevelQuestionCount { get; set; }
        public int HardLevelQuestionCount { get; set; }

        public int ArtQuestionCount { get; set; }
        public int SportsQuestionCount { get; set; }
        public int HistoryQuestionCount { get; set; }
        public int ScienceQuestionCount { get; set; }
        public int EntertainmanetQuestionCount { get; set; }
        public int GeoQuestionCount { get; set; }
        public int Points { get; set; }

        public int PointsInWeek { get; set; }
        public int PointsInMonth { get; set; }

        public string Address { get; set; }
        public string Governement { get; set; }


        public int PlayedGamesCounter { get; set; }

        public int BuysFromStoreCounter { get; set; }

        public int SendGiftsCounter { get; set; }

        public int AddsCounter { get; set; }
        public int RightCounterCounter { get; set; }

        public int InvitationCounter { get; set; }


        public int UploadedQuestion { get; set; }



    }
}
