using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoliceOp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ZeroController : ControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;

        public ZeroController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/<ZeroController>
        [HttpGet("{get-token}")]
        [AllowAnonymous()]
        public string Get()
        {
            var token = jWTService.TokenizeID("89898598", "77a8zeea87", "Session",Models.Issuers.PoliceOpAPI, Models.Audiences.TerminalDesktop);

            return token;
        }


        // POST api/<ZeroController>
        [HttpGet]
        public ActionResult<string> Post([FromHeader] String value)
        {
            Models.Personne P = new Models.Personne()
            {
                Nom = "Eric",
                Prenom = "Hotegni",
                IFU = "888888",
                CouleurCheveux = "Red",
                Sexe = Models.Sexe.M
            };

            string token = jWTService.TokenizeID("6556265", "899d8er6zfxer", "");

            return jWTService.DecodeObjectFromToken(token)["pwd"];


            // return EmbbededData;

            //return Newtonsoft.Json.JsonConvert.SerializeObject(P);


            //return jWTService.DecodeObjectFromToken(token)["iss"].ToString();

        }

    }
}
