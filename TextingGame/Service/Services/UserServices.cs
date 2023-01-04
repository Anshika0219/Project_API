using Domain;
using Domain.RoomModel;
using Domain.UserModel;
using Microsoft.EntityFrameworkCore;
using Persistence.Model;
using Service.Interface;

namespace Service.Services
{
    public class UserServices : EncryptServices, IUserServices
    {
        private readonly DbTextingGameContext _dbUserContext;
        private readonly IEncryptServices _encrypt;
        private readonly IGenrateToken _genrateToken;

        public UserServices(DbTextingGameContext dbUserContext, IEncryptServices encrypt, IGenrateToken genrateToken)
        {
            _dbUserContext = dbUserContext;
            _encrypt = encrypt;
            _genrateToken = genrateToken;
        }

        //...........fetch User detail.........//       
        public List<ListUserRequestModel> GetUsers()
        {
            List<ListUserRequestModel> result = (from user in _dbUserContext.TblUsers
                                                 select new ListUserRequestModel()
                                                 {
                                                     UserName = user.UserName,
                                                     EmailId = user.EmailId,
                                                     MobileNo = user.MobileNo,
                                                 }).ToList();
            if (result.Any())
            {
                return result;
            }
            return new List<ListUserRequestModel>();
        }

        //............Check User Email...........// 

        public BaseResponseModel ValidateUserRequestModel(CreateUserRequestmodel createUserRequestmodel)
        {
            if (CheckUserExist(createUserRequestmodel.EmailId!))
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "User Already Exists"
                };
            }
            if (createUserRequestmodel.Password != createUserRequestmodel.ConfirmPassword)
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Password and Confirm password not match"
                };
            }
            return new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        public bool CheckUserExist(string email)
        {
            var user = _dbUserContext.TblUsers.Where(x => x.EmailId == email).FirstOrDefault();
            return user != null;
        }

        //............Check Password .................//
        public BaseResponseModel Register(CreateUserRequestmodel createUserRequestmodel)
        {
            //Refactor the following code later using Automapper
            //Convert API model to DB model
            EncryptServices encrypt1 = new EncryptServices();

            TblUser tblUser = new TblUser()
            {
                UserName = createUserRequestmodel.Name,
                Password = encrypt1.EncodePasswordToBase64(createUserRequestmodel.Password!),
                MobileNo = createUserRequestmodel.MobileNo,
                EmailId = createUserRequestmodel.EmailId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true
            };
            _dbUserContext.TblUsers.Add(tblUser);
            try
            {
                _dbUserContext.SaveChanges();
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    SuccessMessage = "User created successfully"
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

        //...........User Login.....................//
        public LoginUserResponseModel UserLogIn(LoginUserRequestModel loginUserRequestModel)
        {
            string encryptPassword = _encrypt.EncodePasswordToBase64(loginUserRequestModel.Password!);
            var login = _dbUserContext.TblUsers.Where(x => x.EmailId == loginUserRequestModel.EmailId && x.Password == encryptPassword).FirstOrDefault()!;
            try
            {
                if (login != null)
                {
                    var token = _genrateToken.GenerateToken(login);

                    return new LoginUserResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        SuccessMessage = "User Login SuccessFully",
                        userId = login.UserId,
                        Token = token
                    };
                   
                }
                return new LoginUserResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Invalid User",
                    userId = 0
                };

            }
            catch (Exception ex)
            {
                return new LoginUserResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = string.Format("Login user failed. Exception details are: {0}", ex.Message)
                };
            }

        }

        //...........Forget Password.....................//
        public BaseResponseModel ForgetPassword(UserForgotPasswordRequestModel userForgotPasswordRequestModel)
        {
            var user = _dbUserContext.TblUsers.Where(x => x.EmailId == userForgotPasswordRequestModel.EmailId).FirstOrDefault()!;
            string encryptPassword = _encrypt.EncodePasswordToBase64(userForgotPasswordRequestModel.Password!);
            try
            {
                if (user != null)
                {
                    user.Password = encryptPassword;
                    _dbUserContext.Entry(user).State = EntityState.Modified;
                    _dbUserContext.SaveChanges();
                    return new BaseResponseModel()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        SuccessMessage = "Password reset  Successfully"
                    };
                }
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = "Email Does Not Register"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = string.Format("password reset failed. Exception details are: {0}", ex.Message)
                };
            }

        }
    }
}
