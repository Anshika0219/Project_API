using Domain;
using Domain.Messagemodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Model;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DbTextingGameContext _dbMessageContext;
        private readonly IMessageServices _messageServices;

        public MessageController(DbTextingGameContext dbContext, IMessageServices messageServices)
        {
            _dbMessageContext = dbContext;
            _messageServices = messageServices;
        }

        [HttpGet("GetUserMessage")]
        [Authorize]
        public List<MessageResponse> GetUsersMessage(int RoomId)
        {
            try
            {
                //Validation
                return RoomId == 0 ? new List<MessageResponse>() : _messageServices.GetRoom(RoomId);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }


        [HttpPost("AddUserMessage")]
        public BaseResponseModel AddingMessage(int RoomId, string Message)
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString(Constants.UserId));
            try
            {
                var CheckRoom = _messageServices.CheckRoomId(RoomId);
                //var CheckUser = _messageServices.CheckUserId(UserId);
                if (CheckRoom == false )
                {
                    return new BaseResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        SuccessMessage = "RoomId Not Matched"
                    };
                }
                return _messageServices.AddMessages(RoomId, Message,userid);
          }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

    }
}
