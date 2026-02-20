using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Abstractions.Security;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    int UserId { get; }
}
