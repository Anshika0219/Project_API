using Domain;
using Domain.Messagemodel;

namespace Service.Interface
{
    public interface IMessageServices
    {
        List<MessageResponse> GetRoom(int Roomid);
        bool CheckUserId(int userId);
        bool CheckRoomId(int roomId);
        BaseResponseModel AddMessages(int RoomID, string Message,int userid);
    }
}
