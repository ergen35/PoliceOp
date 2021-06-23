
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PoliceOp.API.Data;
using PoliceOp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AgentsController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;
        private readonly ILogger<AgentsController> logger;

        public AgentsController(PoliceOpAPIContext context, IConfiguration configuration, ILogger<AgentsController> logger)
        {
            _context = context;
            this.configuration = configuration;
            this.logger = logger;
            jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            if (await SessionExists(HttpContext))
            {
                return await _context.Agents.ToListAsync();
            }

            logger.LogInformation("Liste::Agents");
            return Unauthorized("Session ID is Required");
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Agent>> GetAgent(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var agent = _context.Agents.Where(a => a.PersonneId == id).FirstOrDefault();

                if (agent == null)
                {
                    return NotFound();
                }


                await _context.Entry(agent).Reference(a => a.Residence).LoadAsync();
                await _context.Entry(agent).Reference(a => a.Biometrie).LoadAsync();


                logger.LogInformation($"Agent @{agent.Matricule}");

                return agent;
            }

            return Unauthorized("Session ID is Required");
        }

        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Models.Agent>>> SearchForAgent(string keyword)
        {
            if (await SessionExists(HttpContext))
            {
                if (keyword == string.Empty)
                {
                    return new List<Agent>();
                }

                var Results = await _context.Agents.Where(a => a.Nom.Contains(keyword) || a.Prenom.Contains(keyword) || a.Matricule.Contains(keyword) || a.IFU.Contains(keyword) || a.NPI.Contains(keyword)).ToListAsync();

                foreach (var item in Results)
                {
                    await _context.Entry(item).Reference(r => r.Residence).LoadAsync();
                }

                logger.LogInformation($"Liste:: Recherche Agent @Mot-Clé:: {keyword} |");

                return Results;
            }

            return Unauthorized("Session ID Requis");
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.PersonneId == id);
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
