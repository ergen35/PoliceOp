using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoliceOp.API.Data;
using PoliceOp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly PoliceOpAPIContext _context;
        private readonly Services.JWTServices jWTService;
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthController> logger;


        public AuthController(PoliceOpAPIContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            jWTService = new Services.JWTServices(this.configuration);
            _context = context;

        }


        // POST: api/Auth
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Models.SessionVM>> InitiateSession([FromHeader] string Authorization)
        {

            var token = string.Empty;

            if (Authorization != null || Authorization == string.Empty)
            {
                token = Authorization.Split(" ").LastOrDefault();
                //Decode

                var mat = jWTService.DecodeObjectFromToken(token)["mat"];
                var pwd = jWTService.DecodeObjectFromToken(token)["pwd"];

                if ((await _context.Agents.AnyAsync(a => a.Matricule == mat)))
                {
                    var query = from Agent in _context.Agents
                                where Agent.Matricule == mat
                                where Agent.PasswordHash == pwd
                                select Agent;
                    Agent agent = await query.FirstOrDefaultAsync();

                    if (agent != null)
                    {
                        Session newSession = new Session();
                        await _context.Sessions.AddAsync(newSession);
                        await _context.SaveChangesAsync();

                        SessionVM svm = new SessionVM() { SessionID = newSession.SessionID.ToString(), AgentID = agent.PersonneId };

                        logger.LogInformation($"Nouvelle Session Crée Par @{agent.Matricule}, {DateTime.Now}");
                        return Json(svm);
                    }
                    else
                    {
                        logger.LogWarning($"Echec de Création de Session @{agent.Matricule}, {DateTime.Now}, depuis @{HttpContext.Connection.RemoteIpAddress}");
                        return NotFound("Bad Match");
                    }
                }
                else
                {
                    logger.LogError($"Echec de Création de Session @{mat}, {DateTime.Now}, depuis @{HttpContext.Connection.RemoteIpAddress}");

                    return NotFound("No Such Data");
                }
            }
            else
            {
                return BadRequest("Authorization token Not Found");
            }

        }

        // DELETE: api/Auth/5
        [HttpDelete("{uid}")]
        [AllowAnonymous]
        public async Task<ActionResult<Session>> DestroySession(Guid uid)
        {
            var session = await _context.Sessions.FindAsync(uid);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            logger.LogInformation($"Session {session.SessionID} detruite");
            return Ok("Logout successfull");
        }
    }
}
