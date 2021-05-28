using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;


namespace PoliceOp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TerminalController : ControllerBase
    {
        [HttpGet]
        [Route("mobile")]
        public string GetTerminalMobile()
        {

            return "This is the Mobile terminal";
        }

        [HttpGet]
        [Route("desktop")]
        public string GetTerminalDesktop()
        {

            return "This is the Desktop terminal";
        }


    }
}
