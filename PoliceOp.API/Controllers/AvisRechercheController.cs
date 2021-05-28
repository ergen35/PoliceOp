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
            return await _context.AvisRecherches.ToListAsync();
        }

        // GET: api/AvisRecherche/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvisRecherche>> GetAvisRecherche(Guid id)
        {
            var avisRecherche = await _context.AvisRecherches.FindAsync(id);

            if (avisRecherche == null)
            {
                return NotFound();
            }

            return avisRecherche;
        }

        // PUT: api/AvisRecherche/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvisRecherche(Guid id, AvisRecherche avisRecherche)
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

        // POST: api/AvisRecherche
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AvisRecherche>> PostAvisRecherche(AvisRecherche avisRecherche)
        {
            _context.AvisRecherches.Add(avisRecherche);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvisRecherche", new { id = avisRecherche.UID }, avisRecherche);
        }

        // DELETE: api/AvisRecherche/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AvisRecherche>> DeleteAvisRecherche(Guid id)
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

        private bool AvisRechercheExists(Guid id)
        {
            return _context.AvisRecherches.Any(e => e.UID == id);
        }

        private Guid GetSessionIDFromRequest(HttpContext httpContext)
        {
            Guid SessionID = new Guid();

            var AccessToken = httpContext.Request.Headers["SessionID"].ToString();

            if (AccessToken != string.Empty || AccessToken != null )
            {
                if (Guid.TryParse(jWTService.DecodeObjectFromToken(AccessToken)["SessionID"], out SessionID))
                {
                    return SessionID;
                }
            }
             
            return SessionID;
            
        }
    }
}
