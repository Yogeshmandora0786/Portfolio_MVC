using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFDownloadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PDFDownloadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            try
            {
                // Ensure file name is not empty
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return BadRequest("File name is required.");
                }

                // List of possible extensions
                var possibleExtensions = new[] { ".pdf", ".zip" };
                string directory = null;
                string fullFilePath = null;

                // Search for the file with known extensions
                foreach (var extension in possibleExtensions)
                {
                    var possibleFilePathPdf = Path.Combine(_env.ContentRootPath, "FileUpload", "YogeshResume", $"{fileName}{extension}");
                    var possibleFilePathZip = Path.Combine(_env.ContentRootPath, "FileUpload", "ResumePDF", $"{fileName}{extension}");

                    if (System.IO.File.Exists(possibleFilePathPdf))
                    {
                        directory = "YogeshResume";
                        fullFilePath = possibleFilePathPdf;
                        break;
                    }

                    if (System.IO.File.Exists(possibleFilePathZip))
                    {
                        directory = "ResumePDF";
                        fullFilePath = possibleFilePathZip;
                        break;
                    }
                }

                // Check if file was found
                if (fullFilePath == null)
                {
                    return NotFound();
                }

                // Read the file content
                var fileBytes = System.IO.File.ReadAllBytes(fullFilePath);

                // Get the system's Downloads folder path
                var downloadsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                // Save the file to the Downloads folder
                var downloadFilePath = Path.Combine(downloadsFolderPath, Path.GetFileName(fullFilePath));
                System.IO.File.WriteAllBytes(downloadFilePath, fileBytes);

                return Ok(new { FilePath = downloadFilePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
