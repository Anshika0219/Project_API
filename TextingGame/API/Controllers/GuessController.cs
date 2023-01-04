using Domain;
using Domain.GuessUser;
using Domain.RoomModel;
using Domain.UserRoomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Model;
using Service.Interface;
using Service.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessController : ControllerBase
    {
        private readonly DbTextingGameContext _dbGuessContext;
        private readonly IGuessServices _GuessServices;

        public GuessController(DbTextingGameContext dbGuessContext, IGuessServices guessServices)
        {
            _dbGuessContext = dbGuessContext;
            _GuessServices = guessServices;
        }

        [HttpGet]
        [Authorize]
        public List<ListOfResultResponse> GetGuess(int roomId)
        {
            try
            {
                return roomId == 0 ? new List<ListOfResultResponse>() : _GuessServices.GetGuess(roomId);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        [HttpPost]
        public BaseResponseModel createGuess(CreateGuessUserRequestModel createGuessUserRequestModel)
        {
            try
            {
                return _GuessServices.AddGuessUser(createGuessUserRequestModel);

            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

    }
}
