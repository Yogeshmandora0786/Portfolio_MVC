using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;

namespace Portfolio_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMyBlogs _myBlogs;
        private readonly IConfiguration _configuration;
        public BlogsController(IConfiguration configuration, IMyBlogs myBlogs)
        {
            _configuration = configuration;
            _myBlogs = myBlogs;
        }

        [HttpPost]
        [Route("BlogPost_Insert")]
        public async Task<IActionResult> BlogPost_Insert([FromBody] MyBlogs myBlogs)
        {
            try
            {
                var res = await _myBlogs.BlogPost_Insert(myBlogs);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("BlogPost_Update")]
        public async Task<IActionResult> BlogPost_Update(MyBlogs myBlogs)
        {
            try
            {
                var res = await _myBlogs.BlogPost_Update(myBlogs);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("BlogPost_Delete")]
        public async Task<IActionResult> BlogPost_Delete(MyBlogs blogPosts)
        {
            try
            {
                var res = await _myBlogs.BlogPost_Delete(blogPosts);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("BlogPost_All")]

        public async Task<IActionResult> BlogPost_All()
        {
            try
            {
                var res = await _myBlogs.BlogPost_All();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("BlogPost_ById/{id}")]
        public async Task<IActionResult> BlogPost_ById(int id)
        {
            try
            {
                var user = await _myBlogs.BlogPost_ById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        

    }
}
