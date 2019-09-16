using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.JWT.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DH.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private ITokenHelper tokenHelper;
        public TokenController(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }
        public IActionResult Get(string code,string pwd)
        {
            if (string.IsNullOrEmpty(code)||string.IsNullOrEmpty(pwd))
            {
                return BadRequest();
            }
            SysUserer user = new SysUserer
            {
                Code = code,
                Name = "renfushai",
                Pwd = pwd,
            };
            return Ok(tokenHelper.CreateToken(user));
        }
    }
}