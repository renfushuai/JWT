using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH.JWT.Token
{
    public class ComplexToken
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }
}
