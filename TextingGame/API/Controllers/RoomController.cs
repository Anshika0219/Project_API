using Domain;
using Domain.RoomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Model;
using Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly DbTextingGameContext _dbRoomcontext;
        private readonly IRoomServices _roomServices;
        private readonly ISendingSms _sendingSms;

        public RoomController(DbTextingGameContext dbContext, IRoomServices roomServices, ISendingSms sendingSms)
        {
            _dbRoomcontext = dbContext;
            _roomServices = roomServices;
            _sendingSms = sendingSms;
        }

        // GET: api/<RoomController>
        [HttpGet]
       // [Authorize]
        public List<RoomResponse> GetRoom(int userId)
        {
            try
            {
                //Validation
                return userId == 0 ? new List<RoomResponse>() : _roomServices.GetRoom(userId);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPost]
       // [Authorize]
        public BaseResponseModel CreateRoom(CreateRoomRequestModel createRoomRequestModel)
        {
            var userid = Convert.ToInt32 (HttpContext.Session.GetString(Constants.UserId));
            try
            {
                return _roomServices.CreateRoom(createRoomRequestModel,userid);

            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        public BaseResponseModel UpdateRoom(EditRoomRequestModel editRoomRequestModel)
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString(Constants.UserId));
            try
            {
                return _roomServices.UpdateRoom(editRoomRequestModel,userid);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPost("send message")]
        [Authorize]
        public BaseResponseModel SendingSms(double phone, string message)
        {
            try
            {
                return _roomServices.SendSms(phone, message);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
    }
}