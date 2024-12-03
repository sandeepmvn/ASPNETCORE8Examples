using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecondWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnAPIController : ControllerBase
    {
        private readonly TodoDBContext _dbContext;
        public LearnAPIController(TodoDBContext dBContext)
        {
            _dbContext = _dbContext;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(_dbContext.Entity.tp)
        //}
        public string SayHello(string name) => $"Hello {name}";
    }

    public class TodoDBContext
    {
    }
}
