using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesAPIController : ControllerBase
    {

        /*Specfic Type*/
        // GET: api/<ValuesAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //public async Task<IEnumerable<string>> GetAll()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        static List<string> s = new List<string> { "value1", "value2" };

        //[HttpGet("{value}")]
        //[ProducesResponseType(200, Type = typeof(List<string>))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> GetBy(string value)
        //{
        //    try
        //    {
        //        var result = s.Find(x => x == value);
        //        if (result == null)
        //            return NotFound();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("{value}")]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public ActionResult<string> GetBy(string value)
        //{
        //    try
        //    {
        //        var result = s.Find(x => x == value);
        //        if (result == null)
        //            return NotFound();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    };
        //}

        public Results<NotFound, Ok<string>, BadRequest<string>> GetBy(string value)
        {
            try
            {
                var result = s.Find(x => x == value);
                if (result == null)
                    return TypedResults.NotFound();
                return TypedResults.Ok(value);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }


        //[HttpGet("{id}")]
        //[ProducesResponseType<Product>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IResult GetById(int id)
        //{
        //    var product = _productContext.Products.Find(id);
        //    return product == null ? Results.NotFound() : Results.Ok(product);
        //}



        //// GET api/<ValuesAPIController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ValuesAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
