using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DH.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "ASP", "C#" };
        }

        // POST api/<controller>
        [HttpPost]
        public JsonResult Post()
        {
            return new JsonResult("Create  Book ...");
        }
    }
}