using AutoMapper;
using FlCash.Data;
using FlCash.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SignalRSwaggerGen.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Hubs
{
    [SignalRHub(path: "/gamehub")]

    public class GameHub: Hub
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public GameHub(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // users in a game send gameid and recieve all other players in this game
        [SignalRMethod(name: "GameUsersFromServer", operationType: OperationType.Get)]
        public async Task GameUsersFromServer([SignalRArg] long gameId, [SignalRArg] string userId)
        {
            var game = _context.Games.Include(x => x.Questions).Include("Questions.Answers").ToList().FirstOrDefault(x => x.Id == gameId);
           
            var user = _context.Users.Find(userId);

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            //await Clients.OthersInGroup(gameId.ToString()).SendAsync("SendUserEntered", user);
            await Clients.OthersInGroup(gameId.ToString()).SendAsync("SendUserEntered", user);
            if (game.Counter == 2)
            {
                var q = game.Questions.ToList()[0];
                var result = _mapper.Map<QuestionAnswersDTO>(q);
                await Clients.Group(gameId.ToString()).SendAsync("SendQuestion", result);
                //await Clients.Group(gameId.ToString()).SendAsync("SendGameCounter", 1);
            }
 
        }

        public Task LeaveRoom(int roomName)
        {
            string x = roomName.ToString();
            Groups.RemoveFromGroupAsync(Context.ConnectionId, x);
            return null;
        }
        // return the count of players in the game every time a new player added to the game
        [SignalRMethod(name: "GameCounter", operationType: OperationType.Get)]
        public async Task GameCounter([SignalRArg] long gameId)
        {
            var game = _context.Games.Find(gameId);
            await Clients.Group(gameId.ToString()).SendAsync("SendGameCounter",game.Counter);
        }

        // return the count of players in the game every time a new player added to the game
        [SignalRMethod(name: "ResultAnswers", operationType: OperationType.Get)]
        public async Task ResultAnswers([SignalRArg] long gameId, [SignalRArg] long questionId)
        {
            var qs = _context.QuestionResults.Where(x => x.QuestionId == questionId && x.GameId == gameId).FirstOrDefault();

            var m = _mapper.Map<ResultQuestionDTO>(qs);

            await Clients.Caller.SendAsync("GetQuestionAnswers", m);
        }

        // return the count of players in the game every time a new player added to the game
        [SignalRMethod(name: "GameQuestionResult", operationType: OperationType.Get)]
        public async Task GameQuestionResult([SignalRArg] long gameId, [SignalRArg] int index, [SignalRArg] long questionId)
        {

            var quresut = _context.QuestionResults.Where(x => x.GameId == gameId && x.QuestionId == questionId).FirstOrDefault();
            if (quresut != null)
            {
                if (index > 0)
                {
                    if (index == 1)
                    {
                        quresut.CountOfAnswerOne += 1;
                        _context.SaveChanges();
                    }
                    if (index == 2)
                    {
                        quresut.CountOfAnswerTwo += 1;
                        _context.SaveChanges();
                    }
                    if (index == 3)
                    {
                        quresut.CountOfAnswerThree += 1;
                        _context.SaveChanges();
                    }
                    if (index == 4)
                    {
                        quresut.CountOfAnswerFour += 1;
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var qs = _context.QuestionResults.FirstOrDefault(x => x.QuestionId == questionId && x.GameId == gameId);
                    var m = new QuestionResult
                    {
                        CountOfAnswerOne = qs.CountOfAnswerOne,
                        CountOfAnswerTwo = qs.CountOfAnswerTwo,
                        CountOfAnswerThree = qs.CountOfAnswerThree,
                        CountOfAnswerFour = qs.CountOfAnswerFour
                    };

                    await Clients.Caller.SendAsync("GetQuestionAnswers1", m);
                    
                }

            }
            else
            {
                var q = new QuestionResult
                {
                    GameId = gameId,
                    QuestionId = questionId,
                    CountOfAnswerOne = 0,
                    CountOfAnswerTwo = 0,
                    CountOfAnswerThree = 0,
                    CountOfAnswerFour = 0
                };
                if (index == 1)
                {
                    q.CountOfAnswerOne += 1;
                }
                if (index == 2)
                {
                    q.CountOfAnswerTwo += 1;
                }
                if (index == 3)
                {
                    q.CountOfAnswerThree += 1;
                }
                if (index == 4)
                {
                    q.CountOfAnswerFour += 1;
                }
                _context.QuestionResults.Add(q);
                _context.SaveChanges();

            }


        }
           
    }
}
