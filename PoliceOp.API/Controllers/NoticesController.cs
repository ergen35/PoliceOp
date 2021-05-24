using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliceOp.API.Data;
using PoliceOp.Models;

namespace PoliceOp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;

        public NoticesController(PoliceOpAPIContext context)
        {
            _context = context;
        }

        // GET: api/Notices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvisRecherche>>> GetAvisRecherche()
        {
            return await _context.AvisRecherche.ToListAsync();
        }

        // GET: api/Notices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvisRecherche>> GetAvisRecherche(int id)
        {
            var avisRecherche = await _context.AvisRecherche.FindAsync(id);

            if (avisRecherche == null)
            {
                return NotFound();
            }

            return avisRecherche;
        }

        // PUT: api/Notices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvisRecherche(int id, AvisRecherche avisRecherche)
        {
            if (id != avisRecherche.ID)
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

        // POST: api/Notices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AvisRecherche>> PostAvisRecherche(AvisRecherche avisRecherche)
        {
            _context.AvisRecherche.Add(avisRecherche);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvisRecherche", new { id = avisRecherche.ID }, avisRecherche);
        }

        // DELETE: api/Notices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AvisRecherche>> DeleteAvisRecherche(int id)
        {
            var avisRecherche = await _context.AvisRecherche.FindAsync(id);
            if (avisRecherche == null)
            {
                return NotFound();
            }

            _context.AvisRecherche.Remove(avisRecherche);
            await _context.SaveChangesAsync();

            return avisRecherche;
        }

        private bool AvisRechercheExists(int id)
        {
            return _context.AvisRecherche.Any(e => e.ID == id);
        }
    }
}
