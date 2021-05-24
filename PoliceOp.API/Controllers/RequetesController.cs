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
    public class RequetesController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;

        public RequetesController(PoliceOpAPIContext context)
        {
            _context = context;
        }

        // GET: api/Requetes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requete>>> GetRequete()
        {
            return await _context.Requete.ToListAsync();
        }

        // GET: api/Requetes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requete>> GetRequete(int id)
        {
            var requete = await _context.Requete.FindAsync(id);

            if (requete == null)
            {
                return NotFound();
            }

            return requete;
        }

        // PUT: api/Requetes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequete(int id, Requete requete)
        {
            if (id != requete.ID)
            {
                return BadRequest();
            }

            _context.Entry(requete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequeteExists(id))
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

        // POST: api/Requetes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Requete>> PostRequete(Requete requete)
        {
            _context.Requete.Add(requete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequete", new { id = requete.ID }, requete);
        }

        // DELETE: api/Requetes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requete>> DeleteRequete(int id)
        {
            var requete = await _context.Requete.FindAsync(id);
            if (requete == null)
            {
                return NotFound();
            }

            _context.Requete.Remove(requete);
            await _context.SaveChangesAsync();

            return requete;
        }

        private bool RequeteExists(int id)
        {
            return _context.Requete.Any(e => e.ID == id);
        }
    }
}
