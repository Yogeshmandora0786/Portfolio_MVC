using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolio_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        public AdminController(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody]AdminModel admin)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(admin.Email) || string.IsNullOrWhiteSpace(admin.Password))
                {
                    return BadRequest("Email and password are required.");
                }

                var user = await _adminRepository.Admin_Login(admin);

                if (user.Code == "200")
                {
                    var token = GenerateToken(admin);
                    user.Token = token;
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("UserData_ById/{UserId}")]
        public async Task<IActionResult> UserData_ById(int UserId)
        {
            try
            {
                var user = await _adminRepository.UserData_ById(UserId);
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

        [HttpGet]
        [Route("UserData_ByEmail/{Email}")]
        public async Task<IActionResult> UserData_ByEmail(string Email)
        {
            try
            {
                var user = await _adminRepository.UserData_ByEmail(Email);
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

        [HttpPut]
        [Route("ChangePassword_ByEmail")]
        public async Task<IActionResult> ChangePassword_ByEmail(AdminModel admin)
        {
            try
            {
                var user = await _adminRepository.ChangePassword_ByEmail(admin);
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


        private string GenerateToken(AdminModel admin)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,admin.Email),
                new Claim(ClaimTypes.Role,"1")
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpPut]
        [Route("Admin_Upsert")]
        public async Task<IActionResult> Admin_Upsert(AdminModel admin)
        {
            try
            {
                var res = await _adminRepository.Admin_Upsert(admin);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("Admin_ChangePassword")]
        public async Task<IActionResult> Admin_ChangePassword(AdminModel admin)
        {
            try
            {
                var res = await _adminRepository.Admin_ChangePassword(admin);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
