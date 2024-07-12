using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;

namespace Portfolio_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IMyPortfolio _myBlogs;
        private readonly IConfiguration _configuration;
        public PortfolioController(IConfiguration configuration, IMyPortfolio myBlogs)
        {
            _configuration = configuration;
            _myBlogs = myBlogs;
        }

        [HttpPost]
        [Route("Portfolio_Insert")]
        public async Task<IActionResult> Portfolio_Insert([FromBody] MyPortfolio myPortfolio)
        {
            try
            {
                var res = await _myBlogs.Portfolio_Insert(myPortfolio);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("Portfolio_Update")]
        public async Task<IActionResult> Portfolio_Update(MyPortfolio myPortfolio)
        {
            try
            {
                var res = await _myBlogs.Portfolio_Update(myPortfolio);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("Portfolio_Delete")]
        public async Task<IActionResult> Portfolio_Delete(MyPortfolio myPortfolio)
        {
            try
            {
                var res = await _myBlogs.Portfolio_Delete(myPortfolio);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Portfolio_All")]

        public async Task<IActionResult> Portfolio_All()
        {
            try
            {
                var res = await _myBlogs.Portfolio_All();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Portfolio_ById/{id}")]
        public async Task<IActionResult> Portfolio_ById(int id)
        {
            try
            {
                var user = await _myBlogs.Portfolio_ById(id);
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
