using Domain;
using Domain.RoomModel;
using Microsoft.EntityFrameworkCore;
using Persistence.Model;
using Service.Interface;

namespace Service.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly DbTextingGameContext _dbRoomContext;
        private readonly SendingSms sms;

        public RoomServices(DbTextingGameContext dbRoomContext)
        {
            _dbRoomContext = dbRoomContext;
            sms = new SendingSms();
        }

        //...........Fetch Data...........//
        public List<RoomResponse> GetRoom(int userId)
        {
            List<RoomResponse> userRooms = (from userRoom in _dbRoomContext.TblUserRooms
                                            join users in _dbRoomContext.TblUsers on userRoom.UserId equals users.UserId
                                            join rooms in _dbRoomContext.TblRooms on userRoom.RoomId equals rooms.RoomId
                                            where userRoom.UserId == userId
                                            select new RoomResponse()
                                            {
                                                RoomId = userRoom.RoomId ?? 0,
                                                RoomName = rooms.RoomName
                                            }).ToList();
            if (userRooms.Any())
            {
                return userRooms;
            }
            return new List<RoomResponse>();
        }

        public bool CheckExistRoomName(string room)
        {
            var check = _dbRoomContext.TblRooms.Where(x => x.RoomName == room).FirstOrDefault()!;            
            return check != null;
        }

        //...........Create Room..........//
        public BaseResponseModel CreateRoom(CreateRoomRequestModel createRoomRequestModel,int userid)
        {
            try
            {
                bool check = CheckExistRoomName(createRoomRequestModel.RoomName);
                if (check == false)
                {
                    TblRoom room = new TblRoom()
                    {
                        RoomName = createRoomRequestModel.RoomName,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = userid,
                        UpdatedBy = userid
                    };
                    _dbRoomContext.TblRooms.Add(room);
                    _dbRoomContext.SaveChanges();
                    return new BaseResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        SuccessMessage = "Room created successfully"
                    };
                }
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Room Already Exists"
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

        // ............Update Room.............//
        public BaseResponseModel UpdateRoom(EditRoomRequestModel editRoomRequestModel,int userid)
        {
            try
            {
                TblRoom roomUpdate = _dbRoomContext.TblRooms.Where(x => x.RoomId == editRoomRequestModel.RoomId).FirstOrDefault()!;
                if (roomUpdate != null)
                {
                    roomUpdate.RoomName = editRoomRequestModel.RoomName;
                    roomUpdate.UpdatedDate = DateTime.Now;
                    roomUpdate.CreatedDate = DateTime.Now;
                    roomUpdate.CreatedBy = userid;
                    roomUpdate.UpdatedBy = userid;
                    roomUpdate.IsActive = true;
                    _dbRoomContext.Entry(roomUpdate).State = EntityState.Modified;
                    _dbRoomContext.SaveChanges();
                    return new BaseResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        SuccessMessage = "Room Updated successfully"
                    };
                }
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "RoomId Not Exist "
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

        //..............Sending Sms.............//
        public BaseResponseModel SendSms(double phone, string message)
        {
            try
            {
                sms.SendMessage(phone, message);
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    SuccessMessage = "Message send successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = string.Format("Sending sms to user failed. Exception details are: {0}", ex.Message)
                };
            }
        }
    }
}
