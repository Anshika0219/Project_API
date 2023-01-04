using Domain;
using Domain.UserRoomModel;
using Persistence.Model;
using Service.Services;
using Xunit.Sdk;

namespace XUnitTesting
{

    [Collection("DataBase Collection")]
    public class UserRoomServicesTest
    {
        private readonly DatabaseFixure _fixure;
        private readonly UserRoomServices _userservices;
        public UserRoomServicesTest(DatabaseFixure fixure)
        {
            _fixure = fixure;
            _userservices = new UserRoomServices(_fixure._context);
        }
        [Fact]
        public void Get_UserRoomsDetails()
        {
            //Arrange
            var expect = _fixure._context.TblUserRooms.Count();

            //Act
            var result = _userservices.GetUsersRoom(1);
            var items = Assert.IsType<List<ListUserRoomResponse>>(result);

            //Assert
            Assert.Equal(expect, items.Count!);
        }


        [Fact]
        public void Check_New_with_CheckExtistUserRoom()
        {
            //Arrange
            var addingtoRoom = new CreateUserRoomRequestModel()
            {
                RoomId = 2,
                UserId = new int[] {1,2}
            };

            //Act
            var result = _userservices.AddUserToRoom(addingtoRoom,1);
            BaseResponseModel expexted = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                 SuccessMessage = "user Room created successfully"
                   };

            //Assert
            Assert.Equal(result.SuccessMessage, expexted.SuccessMessage);
        }
        [Fact]
        public void Adding_ExsistingUserToRoom()
        {
            //Arrange
            var addingtoRoom = new CreateUserRoomRequestModel()
            {
                RoomId = 1,
                UserId = new int[] {1,2}
            };
            //Act
            var result = _userservices.CheckRoomId(1);
         
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void Delete_newUserFromRoom()
        {
            //Arrange
            var deleteUser = new DeleteRoomRequsetModel()
            {
                RoomId = 6,
                UserId = new int[] {1,2}
            };

            //Act
            var result = _userservices.CheckRoomId(5);

            //Assert
            Assert.False(result);
        }
        [Fact]
        public void Delete_ExistingUserFromRoom()
        {
            //Arrange
            var deleteUser = new DeleteRoomRequsetModel()
            {
                RoomId = 1,
                UserId = new int[] {1,2},
            };

            //Act
            var result = _userservices.DeleteUserFromRoom(deleteUser);
            BaseResponseModel expexted = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                    SuccessMessage = "User Room deleted successfully"
                  };

            //Assert
            Assert.Equal(result.SuccessMessage, expexted.SuccessMessage);
        }

    }
}
