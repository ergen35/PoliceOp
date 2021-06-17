using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliceOp.API.Data;
using PoliceOp.Models;
using PoliceOp.API.Services;
using Microsoft.Extensions.Configuration;

namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiffusionsController : ControllerBase
    {
        private readonly PoliceOpAPIContext _context;
        private readonly IConfiguration configuration;
        private readonly Services.JWTServices jWTService;

        public DiffusionsController(PoliceOpAPIContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
            this.jWTService = new Services.JWTServices(this.configuration);
        }

        // GET: api/Diffusions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diffusion>>> GetDiffusions()
        {

            if (await SessionExists(HttpContext))
            {
                return await _context.Diffusions.ToListAsync();
            }

            return Unauthorized("Session ID is Required");
        }

        // GET: api/Diffusions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diffusion>> GetDiffusion(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var diffusion = await _context.Diffusions.FindAsync(id);

                if (diffusion == null)
                {
                    return NotFound();
                }

                return diffusion;
            }

            return Unauthorized("Session ID is Required");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiffusion(int id, Diffusion diffusion)
        {
            if (await SessionExists(HttpContext))
            {
                if (id != diffusion.DiffusionId)
                {
                    return BadRequest();
                }

                _context.Entry(diffusion).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiffusionExists(id))
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


        [HttpPost]
        public async Task<ActionResult<Diffusion>> PostDiffusion(Diffusion diffusion)
        {
            if (await SessionExists(HttpContext))
            {
                _context.Diffusions.Add(diffusion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDiffusion", new { id = diffusion.DiffusionId }, diffusion);
            }

            return Unauthorized("Session ID is Required");
        }

        // DELETE: api/Diffusions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Diffusion>> DeleteDiffusion(int id)
        {
            if (await SessionExists(HttpContext))
            {
                var diffusion = await _context.Diffusions.FindAsync(id);
                if (diffusion == null)
                {
                    return NotFound();
                }

                _context.Diffusions.Remove(diffusion);
                await _context.SaveChangesAsync();

                return diffusion;
            }

            return Unauthorized("Session ID is Required");
        }

        private bool DiffusionExists(int id)
        {
            return _context.Diffusions.Any(e => e.DiffusionId == id);
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
