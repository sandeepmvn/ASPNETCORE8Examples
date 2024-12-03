using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecondWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ToDoAPIController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TodoItem>))]
        public IActionResult GetAllTodo()
        {
            try
            {
                return Ok(new List<TodoItem> { new TodoItem { Id = "1" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new CustomExceptionResponse
                {
                    Error = ex.Message,
                });
            }
        }

        //[Route("GetTodo/{id}")]
        //[HttpGet]
        //public IActionResult GetTodo([FromRoute]int id)
        //{

        //}

        ////Insert- Post
        ////Put-- Update
        //public IActionResult Post([FromBody] TodoItem item)
        //{
        //}
    }


    public class CustomExceptionResponse
    {
        public string Error { get; set; }
    }
}
