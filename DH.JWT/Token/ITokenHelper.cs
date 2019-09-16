using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH.JWT.Token
{
    public interface ITokenHelper
    {
        Token CreateToken(SysUserer user);
    }
}
