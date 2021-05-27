using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoliceOp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZeroController : Controller
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration config;
        private readonly Data.PoliceOpAPIContext db;
        public ZeroController(Microsoft.Extensions.Configuration.IConfiguration configuration, Data.PoliceOpAPIContext context)
        {
            config = configuration;
            this.db = context;
        }

        [HttpGet]
        public IEnumerable<Models.Requete> GetAll()
        {
            return db.Requetes.ToList();
        }

        [HttpGet("{uid}")]
        public ActionResult<Models.Requete> GetRequete(string uid)
        {
            if (db.Requetes.Any(r => r.UID.ToString() == uid))
            {
                return db.Requetes.FirstOrDefault(r => r.UID.ToString() == uid);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Models.Requete> PostRequete(Models.Requete req)
        {
            if (req != null)
            {
                db.Requetes.Add(req);
                db.SaveChanges();

                return CreatedAtAction(nameof(PostRequete), new { id = req.UID.ToString(), Requete = req });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("anystr")]
        public ActionResult<int> PostAnyString(int id)
        {

            return Json(new {identity = id });
        }
    }
}
