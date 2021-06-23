using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PoliceOp.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    //[Authorize]
    public class ZeroController : Controller
    {

        public Services.JWTServices jWTService { get; set; }
        public IConfiguration configuration { get; set; }
        public Data.PoliceOpAPIContext ctx { get; set; }

        public ZeroController(IConfiguration configuration, Data.PoliceOpAPIContext ctx)
        {
            this.configuration = configuration;
            jWTService = new Services.JWTServices(this.configuration);
            this.ctx = ctx;
        }

        // GET: api/<ZeroController>
        [HttpGet("get-token")]
        public async Task<IActionResult> GetToken()
        {
            var token = jWTService.TokenizeID("89898598", "77a8zeea87", "Session", Models.Issuers.PoliceOpAPI, Models.Audiences.TerminalDesktop);

            //await GenerateData(1500, 450, 22);

            //await ctx.SaveChangesAsync();
            //EraseData();
            //Console.WriteLine("Done!");

            int totalA = ctx.Agents.Count();
            int totalP = ctx.Personnes.Count();

            //return NoContent();

            List<Models.Agent> LA = ctx.Agents.Skip(Faker.RandomNumber.Next(1, 400)).Take(2).AsEnumerable().ToList();

            return Json(new { token = token, totalAgents = totalA, totalPersonnes = totalP, Agebtx2 = LA });
        }

        [HttpGet("get-diff")]
        public ActionResult<IEnumerable<Models.Diffusion>> GetInt()
        {
            return ctx.Diffusions.ToList();
        }

        [HttpGet("get-obj")]
        public IActionResult GetObj()
        {
            return Json(new { Id = 4, GrowRate = 0.002595, Relative = "GPI Rate", Type = "Gross", SearchUID = Guid.NewGuid().ToByteArray() });
        }

        [HttpGet("get-pers")]
        public async Task<ActionResult<Models.Personne>> GetPersonne()
        {
            return Json(await ctx.Personnes.FindAsync(new Random().Next(1, 400)));
        }

        [HttpGet("get-agt")]
        public async Task<ActionResult<Models.Agent>> GetAgent()
        {
            return Json(await ctx.Agents.FindAsync(new Random().Next(1, 100)));
        }

        [HttpGet("get-listAgt")]
        public IEnumerable<Models.Personne> GetListAgent()
        {
            return ctx.Agents.ToList().Take(18);
        }

        [HttpGet("get-Resi")]
        public IEnumerable<Models.Residence> GetListResidences()
        {
            return ctx.Residences.ToList().Skip(58).Take(8);
        }


        // POST api/<ZeroController>
        [HttpPost]
        public ActionResult<string> Post([FromHeader] String value)
        {
            Models.Personne P = new Models.Personne()
            {
                Nom = "Eric",
                Prenom = "Hotegni",
                IFU = "888888",
                CouleurCheveux = "Red",
                Sexe = Models.Sexe.M.ToString()
            };

            string token = jWTService.TokenizeID("6556265", "899d8er6zfxer", "");

            return jWTService.DecodeObjectFromToken(token)["pwd"];

        }

        public async Task GenerateData(long totalPersonnes, long totalAgents, int totalAvis)
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
            for (long i = 0; i < totalAgents; i++)
            {
                await ctx.Agents.AddAsync(await generator.GenerateAgent());

                Console.Write("|");
            }

            Console.WriteLine("Fin Agents\n");


            Console.Write("[");
            for (int i = 0; i < totalAvis; i++)
            {
                await ctx.AvisRecherches.AddAsync(await generator.GenerateAvis());

                Console.Write("|");
            }

            Console.WriteLine("Fin Avis");

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
