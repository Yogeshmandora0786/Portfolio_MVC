using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public FileUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, string filepath = "")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Get the current directory
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "FileUpload", filepath);

                // Ensure the directory exists
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Get the original file name and extension
                var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);

                // Create a new file name using the original name and today's date
                var newFileName = $"{originalFileName}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

                // Combine the directory with the new file name
                var filePath = Path.Combine(uploadDirectory, newFileName);

                // Check if the file already exists and delete it
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }


                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Construct the updated file path relative to the web server
                var updatedFilePath = Path.Combine("FileUpload", filepath, newFileName);

                // Return the updated file path
                return Ok(updatedFilePath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
