using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Admin.Repository;

namespace Portfolio_Admin.Controllers
{
    public class PortfolioController : Controller
    {
        private IClientHelper _clientHelper;
        private PortfolioViewModel _portfolioViewModel;
        private AdminViewModel _adminViewModel;
        private readonly IDataProtector _protector;

        public PortfolioController(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper;
            _portfolioViewModel = new PortfolioViewModel(_clientHelper, dataProtectionProvider);
            _adminViewModel = new AdminViewModel(_clientHelper, dataProtectionProvider);
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public async Task<IActionResult> Index()
        {
            var UserId = Request.Cookies["UserId"];
            var res = await _adminViewModel.GetDataFromUserId(Convert.ToInt32(UserId));
            return View(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddPortfolio(Portfolio portfolio)
        {
            if (portfolio.Portfolio_Image != null)
            {
                var controllerName = this.ControllerContext.ActionDescriptor.ControllerName;
                var res1 = await _portfolioViewModel.UploadFileAsync(portfolio.Portfolio_Image, controllerName);
                portfolio.Portfolio_Image_String = res1;
            }
            else
            {
                // Retrieve UserId from cookie and convert to long
                var UserId = long.Parse(Request.Cookies["UserId"]);

                // Set DeletedBy field of the blog
                portfolio.UpdatedBy = UserId;
            }
            var res = await _portfolioViewModel.AddPortfolio(portfolio);
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortfolio()
        {
            var followersModel = await _portfolioViewModel.GetAllPortfolio();
            return Json(new { data = followersModel });
        }

        public async Task<IActionResult> AllPortfolio()
        {
            return View();
        }

        [HttpDelete]
        public async Task<JsonResult> DeletePortfolio(Portfolio portfolio)
        {
            // Retrieve UserId from cookie and convert to long
            var UserId = long.Parse(Request.Cookies["UserId"]);

            // Set DeletedBy field of the blog
            portfolio.DeletedBy = UserId;

            // Call async method to delete blog
            var res = await _portfolioViewModel.DeleteBlog(portfolio);

            // Return JSON result
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> EditPortfolio(int id)
        {
            var followersModel = await _portfolioViewModel.GetByIdPortfolio(id);
            return View(followersModel);
        }

    }
}
