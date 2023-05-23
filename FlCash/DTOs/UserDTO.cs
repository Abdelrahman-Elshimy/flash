using FlCash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.DTOs
{
    public class BaseUs
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class BaseUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FaceId { get; set; }
        public double Coins { get; set; }
    }
    public class UserRank
    {
        public int Counter { get; set; }
        public string Name { get; set; }
        public double Points { get; set; }
        public string Image { get; set; }

    }
    public class FriendRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class UserDTO: BaseUserDTO
    {

        public virtual IList<FriendDTO> Friends { get; set; }
    }
    public class FrindUserDTO
    {
        public virtual IList<FriendRequest> Friends { get; set; }
        //public int CorrectAnswers { get; set; }
        //public int Tickets { get; set; }
        //public int WiningGameCounter { get; set; }
        //public int WiningQuestionCounter { get; set; }
        //public string Name { get; set; }
        //public UserLevelDTo Level { get; set; }
        //public List<BaseAchievementsDTO> Achievements { get; set; }
    }
    public class ProfileDTO
    {
        public UserLevelDTo Level { get; set; }
        public List<BaseAchievementsDTO> Achievements { get; set; }

        public List<GovernementDto> Governements { get; set; }

        public string name { get; set; }
        public string address { get; set; }
        public string governement { get; set; }

        public string phoneNumber { get; set; }


    }
    public class EditUserDTO
    {

        public string Image { get; set; }
    }
    public class RegisterNewUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FaceId { get; set; }

        public IList<string> FriendsFacesId { get; set; }

    }
    public class Register
    {
        public RegisterNewUser UserModel { get; set; }
    }

    public class UserStore
    {
        public double Coins { get; set; }
        public int CorrectAnswers { get; set; }
        public int Tickets { get; set; }
        public int Hearts { get; set; }
        
    }

    public class UserGiftDaily
    {
        public int Id { get; set; }
        public bool IsCollected { get; set; }
        public bool CanCollect { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public int Value { get; set; }
    }
}
