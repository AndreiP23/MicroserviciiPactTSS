using Microsoft.AspNetCore.Mvc;
using SampleProvider.Models;

namespace SampleProvider.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (id == 666)
            {
                var user = new User
                {
                    Id = id,
                    FirstName = "Ravanelli",
                    LastName = "Lucifer",
                    Email = "rava.lucifer@4226crangasi.com"
                };
                return Ok(user); // Returnează 200 OK cu JSON-ul userului
            }

            return NotFound(); // Returnează 404 dacă cerem alt ID
        }
    }
}
