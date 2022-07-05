using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StatusController : BaseApiController
    {

        [HttpHead]
        public ActionResult CheckAlive(){
            return Ok();
        }
    }
}