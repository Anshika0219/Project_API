using Domain;
using Domain.Messagemodel;
using Domain.UserModel;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesting
{
    [Collection("DataBase Collection")]
    public class MessageServicesTest
    {
        private readonly DatabaseFixure _fixure;
        private readonly MessageServices _messageservices;
        public MessageServicesTest(DatabaseFixure fixure)
        {
            _fixure = fixure;
            _messageservices = new MessageServices(_fixure._context);
        }
        [Fact]
        public void Get_All_Test()
        {
            //Arrange
            var expect = _fixure._context.TblMessages.Count();

            //Act
            var result = _messageservices.GetRoom(1);
            var items = Assert.IsType<List<MessageResponse>>(result);

            //Assert
            Assert.Equal(expect, items.Count!);
        }

        [Fact]
        public void Check_Exist_with_CheckExtistUser()
        {
            var user = new CreateMessageRequestModel()
            {
                RoomId = 1,
                Message = "Hyy",
            };
            //Act
            var result = _messageservices.CheckRoomId(1);
            Assert.True(result);
        }

        [Fact]
        public void check_New_with_checkExistUser()
        {
            //Arrrange
            var user = new CreateMessageRequestModel()
            {
                RoomId = 3,
                Message = "How Are You",
            };

            //Act
            var result = _messageservices.AddMessages(7, "Fine", 3);
            BaseResponseModel expexted = new BaseResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                SuccessMessage = "Message send successfully"
            };

            //Assert
            Assert.Equal(result.SuccessMessage, expexted.SuccessMessage);

        }
    }
}
