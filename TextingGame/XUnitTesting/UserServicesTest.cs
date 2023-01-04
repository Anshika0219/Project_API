using Domain;
using Domain.UserModel;
using Moq;
using Persistence.Model;
using Service.Interface;
using Service.Services;

namespace XUnitTesting
{
    [CollectionDefinition("DataBase Collection")]
    public class DatabaseCollection1 : ICollectionFixture<DatabaseFixure>
    {
    }

    [Collection("DataBase Collection")]
    public class UserServicesTest
    {
        private readonly DatabaseFixure _fixure;
        private readonly UserServices _services;
        Mock<IEncryptServices> _encrypt;
        Mock<IGenrateToken> _genratetoken;

        public UserServicesTest(DatabaseFixure fixure)
        {
            _fixure = fixure;
            _encrypt = new Mock<IEncryptServices>();
            _genratetoken = new Mock<IGenrateToken>();
            _services = new UserServices(_fixure._context, _encrypt.Object, _genratetoken.Object);
        }

       // .........Get all User..........//
        [Fact]
        public void GetAll_Test()
        {
            //Arrange
            var expect = _fixure._context.TblUsers.Count();

            //Act
            var result = _services.GetUsers();
            var items = Assert.IsType<List<ListUserRequestModel>>(result);

            //Assert
            Assert.Equal(expect, items.Count);
        }

        //...........check exist user............//
        [Fact]
        public void Check_Extist_with_CheckExtistUser()
        {
            //Arrange
            var user = new CreateUserRequestmodel()
            {
                Name = "Anshika",
                EmailId = "anshika@gmail.com",
                Password = "anshi12",
                MobileNo = "7867765564"
            };
            _encrypt.Setup(x=>x.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = _services.CheckUserExist(user.EmailId);

            //Assert
            Assert.True(result);
        }


        //..............check new user..............//
        [Fact]
        public void Check_New_with_CheckExtistUser()
        {
            //Arrange
            var user = new CreateUserRequestmodel()
            {
                Name = "shirien",
                EmailId = "shirien@gmail.com",
                Password = "shirien12",
                MobileNo = "7656983422",
            };

            //Act
            var result = _services.Register(user);
            BaseResponseModel excepted = new BaseResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "User created successfully"
            };
            //Assert
            Assert.Equal(result.SuccessMessage,excepted.SuccessMessage);
        }

        //.................check confirm password..........//
        [Fact]
        public void Check_Correct_ConfirmPassword()
        {
            //Arrange
            var user = new CreateUserRequestmodel()
            {
                Name = "pavan",
                EmailId = "pavan@gmail.com",
                Password = "pavan12",
                ConfirmPassword = "pavan12"
            };

            //Act
            var result = _services.CheckUserExist(user.EmailId);
            Assert.True(result);
        }

        //..............check wrong confirm password..........//
        [Fact]
        public void Check_Wrong_ConfirmPassword()
        {
            //Arrange
            var user = new CreateUserRequestmodel()
            {
                Name = "Anshika",
                EmailId = "anshika@gmail.com",
                Password = "anshi12",
                ConfirmPassword = "4321"
            };
            _encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);
            //Act
            var result = _services.CheckUserExist(user.EmailId);
            Assert.True(result);
        }

        //..........login with correct mail and password.........//
        [Fact]
        public void LogIn_with_correct_mail_password()
        {
            //Arrange
            var login = new LoginUserRequestModel()
            {
                EmailId = "anshika@gmail.com",
                Password = "anshi12",
            };
            _encrypt.Setup(method => method.EncodePasswordToBase64(login.Password)).Returns(login.Password);
            //Act

            var result = _services.UserLogIn(login);
            BaseResponseModel excepted = new BaseResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "User Login SuccessFully"
            };
            //Assert
            Assert.Equal(result.SuccessMessage, excepted.SuccessMessage);
        }

        //.................check login with correct mail and wrong password..........//
        [Fact]
        public void LogIn_with_correct_mail_wrong_password()
        {
            //Arrange
            var login = new LoginUserRequestModel()
            {
                EmailId = "anshika@gmail.com",
                Password = "an1234",
            };
            _encrypt.Setup(method => method.EncodePasswordToBase64(login.Password)).Returns(login.Password);

            //Act
            var result = _services.UserLogIn(login);
            BaseResponseModel excepted = new BaseResponseModel
            {
                     StatusCode = System.Net.HttpStatusCode.BadRequest,
                     ErrorMessage = "Invalid User"
        };
            //Assert
            Assert.Equal(result.ErrorMessage, excepted.ErrorMessage);
        }

        //.............check login with new mail...............//
        [Fact]
        public void LogIn_with_new_mail()
        {
            //Arrange
            var login = new LoginUserRequestModel()
            {
                EmailId = "pavan12@gmail.com",
                Password = "pava123",
            };
            _encrypt.Setup(method => method.EncodePasswordToBase64(login.Password)).Returns(login.Password);

            var result = _services.UserLogIn(login);
            BaseResponseModel excepted = new BaseResponseModel
            {
                 StatusCode = System.Net.HttpStatusCode.BadRequest,
                     ErrorMessage = "Invalid User"
                
        };
            //Assert
            Assert.Equal(result.ErrorMessage, excepted.ErrorMessage);
        }

        //..................check forgot password..............//
        [Fact]
        public void Forget_Password_Test()
        {
            //Arrange
            var pwd = new UserForgotPasswordRequestModel()
            {
                EmailId = "anshika@gmail.com",
                Password = "an123",
            };
            _encrypt.Setup(method => method.EncodePasswordToBase64(pwd.Password)).Returns(pwd.Password);

            //Act
            var result = _services.ForgetPassword(pwd);
            BaseResponseModel excepted = new BaseResponseModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "Password reset  Successfully"
            };
            //Assert
            Assert.Equal(result.SuccessMessage, excepted.SuccessMessage);
        }
    }
}



