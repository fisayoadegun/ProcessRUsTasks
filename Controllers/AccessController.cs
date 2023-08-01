using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProcessRUsTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        
        [HttpGet("Get")]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = ("Admin, BackOffice"), AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            // Create an array of fruit types
            string[] fruitTypes = new string[]
            {
                "Apple",
                "Orange",
                "Banana",
                "Mango",
                "Grapes"
            };

            // Return the array of fruit types as JSON
            return Ok(fruitTypes);
        }
    }
}
