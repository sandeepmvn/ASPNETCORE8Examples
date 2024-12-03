using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecondWebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFormController : ControllerBase
    {

        /*
         * Use x-www-form-urlencoded when:

Sending simple key-value pairs
Data is primarily text-based
You're working with older systems or simple forms
The amount of data is relatively small
         */
        // Handling x-www-form-urlencoded data
        [HttpPost("urlencoded")]
        public IActionResult HandleUrlEncoded([FromForm] UrlEncodedModel model)
        {
            return Ok($"Received: {model.Key1}, {model.Key2}");
        }



        /*
         * Use multipart/form-data when:

Uploading files along with other form data
Sending binary data
Dealing with larger amounts of data
You need to maintain the file's content type
         */
        // Handling multipart/form-data
        [HttpPost("multipart")]
        public async Task<IActionResult> HandleMultipart([FromForm] MultipartModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                // Process the file here
                // For example, save it to a specific location
                var filePath = Path.Combine("uploads", model.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
            }

            return Ok($"Received: {model.Key1}, File: {model.File?.FileName}");
        }

    }

    public class UrlEncodedModel
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }
    }

    public class MultipartModel
    {
        public string Key1 { get; set; }
        public IFormFile File { get; set; }
    }
}
