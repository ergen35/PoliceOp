using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoliceOp.API.Data;
using PoliceOp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AvisRechercheController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;

        public AvisRechercheController(PoliceOpAPIContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
            _context = context;
        }

        // GET: api/AvisRecherche
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvisRecherche>>> GetAvisRecherches()
        {
            if (await SessionExists(HttpContext))
            {
                var liar = await _context.AvisRecherches.ToListAsync();

                foreach (var item in liar)
                {
                    await _context.Entry(item).Reference(r => r.PersonneRecherchee).LoadAsync();
                }
                return liar;
            }

            return Unauthorized("Session ID is Required");
        }

        // GET: api/AvisRecherche/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvisRecherche>> GetAvisRecherche(Guid id)
        {
            if (await SessionExists(HttpContext))
            {
                var avisRecherche = await _context.AvisRecherches.FindAsync(id);

                if (avisRecherche == null)
                {
                    return NotFound();
                }

                return avisRecherche;

            }

            return Unauthorized("Session ID is Required");
        }

        // PUT: api/AvisRecherche/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvisRecherche(Guid id, AvisRecherche avisRecherche)
        {

            if (await SessionExists(HttpContext))
            {
                if (id != avisRecherche.UID)
                {
                    return BadRequest();
                }

                _context.Entry(avisRecherche).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisRechercheExists(id))
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

        // POST: api/AvisRecherche
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AvisRecherche>> PostAvisRecherche(AvisRecherche avisRecherche)
        {
            if (await SessionExists(HttpContext))
            {
                _context.AvisRecherches.Add(avisRecherche);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAvisRecherche", new { id = avisRecherche.UID }, avisRecherche);
            }

            return Unauthorized("Session ID is Required");
        }

        // DELETE: api/AvisRecherche/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AvisRecherche>> DeleteAvisRecherche(Guid id)
        {

            if (await SessionExists(HttpContext))
            {
                var avisRecherche = await _context.AvisRecherches.FindAsync(id);
                if (avisRecherche == null)
                {
                    return NotFound();
                }

                _context.AvisRecherches.Remove(avisRecherche);
                await _context.SaveChangesAsync();

                return avisRecherche;
            }

            return Unauthorized("Session ID is Required");
        }

        private bool AvisRechercheExists(Guid id)
        {
            return _context.AvisRecherches.Any(e => e.UID == id);
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
