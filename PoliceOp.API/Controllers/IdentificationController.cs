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
    public class IdentificationController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;

        public IdentificationController(PoliceOpAPIContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/Identification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            if (await SessionExists(HttpContext))
            {
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

                return personne;
            }

            return Unauthorized("Session ID is Required");

        }

        // PUT: api/Identification/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {

            if (await SessionExists(HttpContext))
            {
                if (id != personne.PersonneId)
                {
                    return BadRequest();
                }

                _context.Entry(personne).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(id))
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

        // POST: api/Identification
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            if (await SessionExists(HttpContext))
            {
                _context.Personnes.Add(personne);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPersonne", new { id = personne.PersonneId }, personne);
            }

            return Unauthorized("Session ID is Required");
        }

        // DELETE: api/Identification/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personne>> DeletePersonne(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var personne = await _context.Personnes.FindAsync(id);
                if (personne == null)
                {
                    return NotFound();
                }

                _context.Personnes.Remove(personne);
                await _context.SaveChangesAsync();

                return personne;
            }

            return Unauthorized("Session ID is Required");

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
