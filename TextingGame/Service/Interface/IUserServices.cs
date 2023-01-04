using Domain;
using Domain.UserModel;

namespace Service.Interface
{
    public interface IUserServices
    {
        List<ListUserRequestModel> GetUsers();
        bool CheckUserExist(string email);
        BaseResponseModel Register(CreateUserRequestmodel register);
        LoginUserResponseModel UserLogIn(LoginUserRequestModel loginUserRequestModel);
        BaseResponseModel ForgetPassword(UserForgotPasswordRequestModel userForgotPasswordRequestModel);
        BaseResponseModel ValidateUserRequestModel(CreateUserRequestmodel createUserRequestmodel);
    }
}
