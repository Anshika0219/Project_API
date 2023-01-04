using Domain;
using Domain.GuessUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IGuessServices
    {
        List<ListOfResultResponse> GetGuess(int roomId);
        BaseResponseModel AddGuessUser(CreateGuessUserRequestModel createGuessUserRequestModel);
        BaseResponseModel validateUserRequestModel(CreateGuessUserRequestModel createGuessUserRequestModel);
    }
}
