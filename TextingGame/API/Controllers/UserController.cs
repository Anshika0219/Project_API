using Domain;
using Domain.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Model;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbTextingGameContext _dbUserContext;
        private readonly IUserServices _userService;
        public UserController(DbTextingGameContext dbUserContext, IUserServices iuserDetail)
        {
            _dbUserContext = dbUserContext;
            _userService = iuserDetail;
        }

        [HttpGet]
      //  [Authorize]
        public List<ListUserRequestModel> GetUsers()
        {
            //retrieve session variables here
            

            try
            {
                return _userService.GetUsers().ToList();
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPost("UserRegister")]
        public BaseResponseModel UserRegister(CreateUserRequestmodel createUserRequestmodel)
        {
            try
            {
                //Validate the incoming model
                BaseResponseModel errorModel = _userService.ValidateUserRequestModel(createUserRequestmodel);

                //check to see if the validation has failed
                if (errorModel.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return errorModel;
                }
                return _userService.Register(createUserRequestmodel);
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

        [HttpPost("LogIn")]
        public LoginUserResponseModel UserLogIn(LoginUserRequestModel loginUserRequestModel)
        {
            try
            {
                LoginUserResponseModel response = _userService.UserLogIn(loginUserRequestModel);

                //set session value
                HttpContext.Session.SetString(Constants.UserId, response.userId.ToString());
                return response;
            }
            catch (Exception ex)
            {
                return new LoginUserResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = string.Format("Creating an user failed. Exception details are: {0}", ex.Message)
                };
            }
        }

        [HttpPut("ForgetPassword")]
        //[Authorize]
        public BaseResponseModel ForgetPassword(UserForgotPasswordRequestModel userForgotPasswordRequestModel)
        {
            try
            {
                return _userService.ForgetPassword(userForgotPasswordRequestModel);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
    }
}
