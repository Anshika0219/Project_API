using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserModel
{
    public class LoginUserResponseModel : BaseResponseModel
    {
        public int userId { get; set; }
        public string? Token { get; set; }
    }
}
