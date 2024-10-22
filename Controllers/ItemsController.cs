using Microsoft.AspNetCore.Mvc;

namespace FullStackApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Item1", "Item2", "Item3" };
        }
    }
}
