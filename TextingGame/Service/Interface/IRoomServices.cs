using Domain;
using Domain.RoomModel;
using Persistence.Model;

namespace Service.Interface
{
    public interface IRoomServices
    {
        List<RoomResponse> GetRoom(int userId);
        bool CheckExistRoomName(string room);
        BaseResponseModel CreateRoom(CreateRoomRequestModel createRoomRequestModel,int userid);
        BaseResponseModel UpdateRoom(EditRoomRequestModel editRoomRequestModel,int userid);
        BaseResponseModel SendSms(double phone, string message);
    }
}
