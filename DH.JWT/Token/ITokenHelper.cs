using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DH.JWT.Token
{
    public interface ITokenHelper
    {
        Token CreateAccessToken(SysUserer user);
        ComplexToken CreateToken(SysUserer user);
        Token RefreshToken(ClaimsPrincipal claimsPrincipal);
    }
}
