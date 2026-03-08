using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.UserActionToken;

public interface IUserActionTokenHelper
{
    public string CreateActionToken(int length = 32);
}
