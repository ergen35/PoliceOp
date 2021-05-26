using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
namespace PoliceOp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet]
        public string GetToken()
        {
            return new Services.JWTServices(this._configuration).GenerateTokenFromObject(new Guid().ToString());
        }

        [HttpPost]
        public JsonResult GetEncodedObject(string token)
        {
            return Json(new Services.JWTServices(this._configuration).DecodeObjectFromToken(token));
        }

    }
}
