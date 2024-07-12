using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Admin.Repository;

namespace Portfolio_Admin.Controllers
{
    public class HomeController : Controller
    {
        private IClientHelper _clientHelper;
        private AdminViewModel _adminViewModel;
        private readonly IDataProtector _protector;

        public HomeController(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper;
            _adminViewModel = new AdminViewModel(_clientHelper, dataProtectionProvider);
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> AdminLogin(Admin admin)
        {
            try
            {
                var res = await _adminViewModel.AdminLogin(admin);
                if (res != null)
                {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1);
                    cookieOptions.Path = "/";
                    JArray jsonArray = JArray.Parse(res.ResponseData);
                    JObject jsonObject = (JObject)jsonArray[0];
                    int id = (int)jsonObject["Id"];
                    string firstName = (string)jsonObject["FirstName"];
                    string idAsString = id.ToString();
                    Response.Cookies.Append("UserId", idAsString, cookieOptions);
                    HttpContext.Session.SetString("FirstName", firstName);
                    ViewBag.FirstName = firstName; // Add this line
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return Json(500, errorMessage);
            }
        }


        public ActionResult Logout()
        {
           
            HttpContext.Session.Remove("FirstName");
            ViewBag.FirstName = null; 

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append("UserId", "", options);

            return Redirect("~/Home/Index");
        }

    }
}
