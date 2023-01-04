using Domain;
using Domain.RoomModel;
using Persistence.Model;
using Service.Interface;
using Service.Services;
using Xunit.Sdk;

namespace XUnitTesting
{
    [Collection("DataBase Collection")]
    public class RoomServicesTest
    {
        private readonly DatabaseFixure _fixure;
        private readonly RoomServices _Roomservices;
        public RoomServicesTest(DatabaseFixure fixure)
        {
            _fixure = fixure;
            _Roomservices = new RoomServices(_fixure._context);
        }
        [Fact]
        public void GetAll_roomDetails_Using_UserId()
        {
            //Arrange
           // var expected = _fixure._context.TblRooms.Count();
            //Act
            var result = _Roomservices.GetRoom(1);
            var items = Assert.IsType<List<RoomResponse>>(result);
            //Assert
            Assert.Equal(1, items.Count!);
        }

        [Fact]
        public void CreateRoom_With_NewUser()
        {
            //Arrange
            var Room = new CreateRoomRequestModel()
            {
                RoomName = "Denish",
            };
            //Act
            var result = _Roomservices.CreateRoom(Room,1);
            BaseResponseModel expected = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "Room created successfully"
            };
            //Assert
            Assert.Equal(result.SuccessMessage, expected.SuccessMessage);
        }

        [Fact]
        public void CreateRoom_Check_User_AlreadyExist()
        {
            //Arrange
            var Room = new CreateRoomRequestModel()
            {
                RoomName = "FunGame",
            };
            //Act
            var result = _Roomservices.CreateRoom(Room, 1);
            BaseResponseModel expexted = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = "Room Already Exists"
            };
            //Assert
            Assert.Equal(result.ErrorMessage, expexted.ErrorMessage);
        }

        [Fact]
        public void UpdateRoom_ByUsing_RoomId()
        {
            //Arrange
            var updateRoom = new EditRoomRequestModel()
            {
                RoomId = 2,
                RoomName = "TextGame"
            };
            //Act
            var result = _Roomservices.UpdateRoom(updateRoom,2);
            BaseResponseModel expected = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "Room Updated successfully"
            };
            //Assert
            Assert.Equal(result.SuccessMessage, expected.SuccessMessage);
        }

        [Fact]
        public void UpdateRoom_ByUsing_WrongRoomId()
        {
            //Arrange
            var updateRoom = new EditRoomRequestModel()
            {
                RoomId = 3,
                RoomName = "pubgGame"
            };
            //Act
            var result = _Roomservices.UpdateRoom(updateRoom,1);
            BaseResponseModel expected = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessage = "RoomId Not Exist"
            };
            //Assert
            Assert.NotEqual(result.ErrorMessage, expected.ErrorMessage);
        }
    }
}

