using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH.JWT.Token
{
    public class Token
    {
        public string TokenContent { get; set; }
        public DateTime Expires { get; set; }
    }
}
