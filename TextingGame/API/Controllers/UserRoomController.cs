using Domain;
using Domain.UserRoomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Model;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoomController : ControllerBase
    {
        private readonly DbTextingGameContext _dbUserRoomContext;
        private readonly IUserRoomServices _userRoomServices;

        public UserRoomController(DbTextingGameContext dbContext, IUserRoomServices userRoomServices)
        {
            _dbUserRoomContext = dbContext;
            _userRoomServices = userRoomServices;
        }

        [HttpGet("GetUsersRoom")]
        [Authorize]
        public List<ListUserRoomResponse> GetUsersRoom(int roomId)
        {
            try
            {
                //Validation
                return roomId == 0 ? new List<ListUserRoomResponse>() : _userRoomServices.GetUsersRoom(roomId);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPost("AddUserToRoom")]
        public BaseResponseModel ValidateUserRequestModel(CreateUserRoomRequestModel createUserRoomRequestModel)
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString(Constants.UserId));
            try
            {
                BaseResponseModel errorModel = _userRoomServices.ValidateUserRequestModel(createUserRoomRequestModel);
                if (errorModel.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return errorModel;
                }
                return _userRoomServices.AddUserToRoom(createUserRoomRequestModel,userid);

            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]

        public BaseResponseModel ValidateUserRequestModel(DeleteRoomRequsetModel deleteRoomRequsetModel)
        {
            try
            {
                BaseResponseModel errorModel = _userRoomServices.ValidateUserRequestModel(deleteRoomRequsetModel);
                if (errorModel.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return errorModel;
                }
                return _userRoomServices.DeleteUserFromRoom(deleteRoomRequsetModel);

            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
    }
}
