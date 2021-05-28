using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliceOp.API.Data;
using PoliceOp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;


namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly Services.JWTServices jWTService;
        private readonly IConfiguration configuration;


        public AuthController(PoliceOpAPIContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
            _context = context;

        }


        // POST: api/Auth
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Models.Session>> InitiateSession([FromHeader] string Authorization)
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

                    if ((await query.FirstOrDefaultAsync()) != null)
                    {
                        Session newSession = new Session();
                        await _context.Sessions.AddAsync(newSession);
                        await _context.SaveChangesAsync();
                        
                        return newSession;
                    }
                    else
                    {
                        return NotFound("Bad Math");
                    }
                }
                else
                {
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

            return session;
        }
    }
}
