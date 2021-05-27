using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TerminalController : Controller
    {
        [HttpGet]
        [Route("mobile")]
        public ActionResult GetTerminalMobile()
        {

            return Json(new { x = "This is the Mobile terminal" }) ;
        }

        [HttpGet]
        [Route("desktop")]
        public ActionResult GetTerminalDesktop()
        {

            return Json(new { x = "This is the Desktop terminal" });
        }


    }
}
