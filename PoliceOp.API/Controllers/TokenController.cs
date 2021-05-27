using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PoliceOp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration configuration;
        public Services.JWTServices JWTServices { get; set; }

        public List<string> LisT { get; set; }

        public TokenController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.JWTServices = new Services.JWTServices(this.configuration);
            this.LisT = new List<string>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            string a = JWTServices.GenerateTokenFromObject<string>("Hello");

            return Json(new {token = a, data = JWTServices.DecodeObjectFromToken<string>(a) });
        }

        [HttpPost]
        public ActionResult<string> PostToken(string jeton)
        {
            if (jeton == null)
            {
                return "Pas marché";
            }


            return jeton + " Marché";
        }
    }
}
