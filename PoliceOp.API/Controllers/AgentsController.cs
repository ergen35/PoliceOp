
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


        public AgentsController(PoliceOpAPIContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            if (await SessionExists(HttpContext))
            {
                return await _context.Agents.ToListAsync();
            }

            return Unauthorized("Session ID is Required");
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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

                return agent;
            }

            return Unauthorized("Session ID is Required");
        }

        // PUT: api/Agents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, Agent agent)
        {

            if (await SessionExists(HttpContext))
            {
                if (id != agent.PersonneId)
                {
                    return BadRequest();
                }

                _context.Entry(agent).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            return Unauthorized("Session ID is Required");

        }

        // POST: api/Agents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(Agent agent)
        {
            if (await SessionExists(HttpContext))
            {

                _context.Agents.Add(agent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAgent", new { id = agent.PersonneId }, agent);
            }

            return Unauthorized("Session ID is Required");
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Agent>> DeleteAgent(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var agent = await _context.Agents.FindAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }

                _context.Agents.Remove(agent);
                await _context.SaveChangesAsync();

                return agent;
            }

            return Unauthorized("Session ID is Required");
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
