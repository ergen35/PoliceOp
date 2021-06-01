using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoliceOp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ZeroController : Controller
    {

        public Services.JWTServices jWTService { get; set; }
        public IConfiguration configuration { get; set; }
        public Data.PoliceOpAPIContext ctx { get; set; }

        public ZeroController(IConfiguration configuration, Data.PoliceOpAPIContext ctx)
        {
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
            this.ctx = ctx;
        }

        // GET: api/<ZeroController>
        [HttpGet("{get-token}")]
        [AllowAnonymous()]
        public async Task<IActionResult> Get()
        {
            var token = jWTService.TokenizeID("89898598", "77a8zeea87", "Session",Models.Issuers.PoliceOpAPI, Models.Audiences.TerminalDesktop);

            //await GenerateData();
            
            int totalA = ctx.Agents.Count();
            int totalP = ctx.Personnes.Count();

            return Json(new {token = token, totalAgents = totalA, totalPersonnes = totalP});
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

        public async Task GenerateData()
        {
            //ctx.Personnes.RemoveRange(ctx.Personnes.AsEnumerable());
            //ctx.Agents.RemoveRange(ctx.Agents.AsEnumerable());

            //ctx.SaveChanges();

            Console.WriteLine("Removed All");

            DataGenerator generator = new DataGenerator();

            Console.Write("[");

            for (int i = 0; i < 1000; i++)
            {
                await ctx.AddAsync(await generator.GeneratePersonne());
                Console.Write("|");
            }

            Console.WriteLine("Fin Personnes\n");

            Console.Write("[");
            for (int i = 0; i < 200; i++)
            {
                await ctx.AddAsync(await generator.GenerateAgent());

                Console.Write("|");
            }

            Console.WriteLine("Fin Agents");


            await ctx.SaveChangesAsync();

        }

    }
}
