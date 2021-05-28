using Microsoft.AspNetCore.Mvc;
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
