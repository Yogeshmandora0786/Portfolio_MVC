using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Admin.Repository;

namespace Portfolio_Admin.Controllers
{
    public class DashbordController : Controller
    {
        private IClientHelper _clientHelper;
        private BlogsViewModel _blogsViewModel;
        private AdminViewModel _adminViewModel;
        private PortfolioViewModel _portfolioViewModel;
        private readonly IDataProtector _protector;

        public DashbordController(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper;
            _adminViewModel = new AdminViewModel(_clientHelper, dataProtectionProvider);
            _blogsViewModel = new BlogsViewModel(_clientHelper, dataProtectionProvider);
            _portfolioViewModel = new PortfolioViewModel(_clientHelper, dataProtectionProvider);
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ReadMoreBlogs(int id)
        {
            var followersModel = await _blogsViewModel.GetByIdBlog(id);
            return View(followersModel);
        }

        
        public async Task<IActionResult> ReadMorePortfolio(int id)
        {
            var followersModel = await _portfolioViewModel.GetByIdPortfolio(id);
            return View(followersModel);
        }



    }
}
