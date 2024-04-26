using ClassTest.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ClassDbContext _context;

        public ClassController(ClassDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClasses()
        {
            return Ok(_context.Classes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetClass([FromRoute] string id)
        {
            return Ok(_context.Classes.Where(c => c.Id.Equals(id)).FirstOrDefault());
        }
    }
}
