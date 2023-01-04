using Domain;
using Domain.GuessUser;
using Persistence.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GuessServices : IGuessServices
    {
        private readonly DbTextingGameContext _dbGuessContext;

        public GuessServices(DbTextingGameContext dbGuessContext)
        {
            _dbGuessContext = dbGuessContext;
        }
        public List<ListOfResultResponse> GetGuess(int roomid)
        {
            List<ListOfResultResponse> guess = (from userRoom in _dbGuessContext.TblUserRooms
                                                join users in _dbGuessContext.TblUsers on userRoom.UserId equals users.UserId
                                                join rooms in _dbGuessContext.TblRooms on userRoom.RoomId equals rooms.RoomId
                                                join userrooms in _dbGuessContext.TblUserRooms on userRoom.UserRoomId equals userrooms.UserRoomId
                                                //where userRoom.RoomId == roomId
                                                select new ListOfResultResponse()
                                                {
                                                    RoomId = userRoom.RoomId ?? 0,
                                                    UserName = users.UserName,
                                                    Guess = userrooms.Score,
                                                }).ToList();
            if (guess.Any())
            {
                return guess;
            }
            return new List<ListOfResultResponse>();
        }
        public bool CheckUserExist(int? userId)
        {
            var user = _dbGuessContext.TblUsers.Where(x => x.UserId == userId).FirstOrDefault();
           return user != null;
        }
        public BaseResponseModel validateUserRequestModel(CreateGuessUserRequestModel createGuessUserRequestModel)
        { 
            if (CheckUserExist(createGuessUserRequestModel.UserId))
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    SuccessMessage = "User Guess succesfully"
                };
            }
            return new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = "user id is not matched"
            };
        }
        public BaseResponseModel AddGuessUser(CreateGuessUserRequestModel createGuessUserRequestModel)
        {
            TblUserRoom tblUserRoom = new TblUserRoom()
            {
                UserId = createGuessUserRequestModel.UserId,
                ImpersonatedUserId = createGuessUserRequestModel.ImpersonatedUserId,
            };
            _dbGuessContext.TblUserRooms.Add(tblUserRoom);
            try
            {
                _dbGuessContext.SaveChanges();
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    SuccessMessage = "User guess successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = string.Format("Creating an user failed. Exception details are: {0}", ex.Message)
                };
            }
        }
    }
}
