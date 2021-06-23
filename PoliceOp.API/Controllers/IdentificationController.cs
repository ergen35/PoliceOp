using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoliceOp.API.Data;
using PoliceOp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentificationController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;
        private readonly ILogger<IdentificationController> logger;

        public IdentificationController(PoliceOpAPIContext context, IConfiguration configuration, ILogger<IdentificationController> logger)
        {
            _context = context;
            this.logger = logger;
            this.configuration = configuration;
            jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/Identification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            if (await SessionExists(HttpContext))
            {
                logger.LogInformation("Liste:: Personnes");
                return await _context.Personnes.ToListAsync();
            }

            return Unauthorized("Session ID is Required");

        }

        // GET: api/Identification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var personne = await _context.Personnes.FindAsync(id);

                if (personne == null)
                {
                    return NotFound();
                }

                await _context.Entry(personne).Reference(p => p.Residence).LoadAsync();
                await _context.Entry(personne).Reference(p => p.Biometrie).LoadAsync();

                logger.LogInformation($"Personne:: {personne.UID}");
                return personne;
            }

            return Unauthorized("Session ID is Required");

        }

        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Personne>>> SearchFor(string keyword)
        {
            if (await SessionExists(HttpContext))
            {
                if (keyword == string.Empty)
                {
                    return new List<Personne>();
                }

                var Results = await _context.Personnes.Where(p => p.Nom.Contains(keyword) || p.Prenom.Contains(keyword) || p.IFU.Contains(keyword) || p.NPI.Contains(keyword)).ToListAsync();

                foreach (var item in Results)
                {
                    await _context.Entry(item).Reference(r => r.Residence).LoadAsync();
                }

                logger.LogInformation($"Liste:: Recherche Personne @Mot-Clé:: {keyword} |");

                return Results;
            }

            return Unauthorized("Session ID Requis");
        }

        private bool PersonneExists(int id)
        {
            return _context.Personnes.Any(e => e.PersonneId == id);
        }

        private async Task<bool> SessionExists(HttpContext httpContext)
        {

            var AccessToken = jWTService.GetTokenFromRequest(httpContext);

            if (AccessToken != null)
            {
                var sessionID = jWTService.DecodeObjectFromToken(AccessToken)["sid"];

                if ((await _context.Sessions.AnyAsync(s => s.SessionID.ToString() == sessionID)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
