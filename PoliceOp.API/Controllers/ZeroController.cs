using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            //await GenerateData(500, 100);

            //EraseData();

            //int totalA = ctx.Agents.Count();
            //int totalP = ctx.Personnes.Count();

            List<Models.Agent> LA = ctx.Agents.Take(2).AsEnumerable().ToList();

            //return Json(new {token = token, totalAgents = totalA, totalPersonnes = totalP});

            return Json(LA);
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

        }

        public async Task GenerateData(long totalPersonnes, long totalAgents)
        {
            DataGenerator generator = new DataGenerator();

            Console.Write("[");

            for (long i = 0; i < totalPersonnes; i++)
            {
                await ctx.Personnes.AddAsync(await generator.GeneratePersonne());
                Console.Write("|");
            }

            Console.WriteLine("Fin Personnes\n");

            Console.Write("[");
            for (long i = 0; i < totalPersonnes; i++)
            {
                await ctx.Agents.AddAsync(await generator.GenerateAgent());

                Console.Write("|");
            }

            Console.WriteLine("Fin Agents");


            ctx.SaveChanges();

        }

        public void EraseData()
        {
            ctx.Personnes.RemoveRange(ctx.Personnes.AsEnumerable());
            ctx.Agents.RemoveRange(ctx.Agents.AsEnumerable());

            ctx.SaveChanges();

            Console.WriteLine("Removed All");
        }

    }
}
