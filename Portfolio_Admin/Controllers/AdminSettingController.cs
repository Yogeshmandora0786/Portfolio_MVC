using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Admin.Repository;

namespace Portfolio_Admin.Controllers
{
    public class AdminSettingController : Controller
    {
        private IClientHelper _clientHelper;
        private AdminViewModel _adminViewModel;
        private readonly IDataProtector _protector;

        public AdminSettingController(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper;
            _adminViewModel = new AdminViewModel(_clientHelper, dataProtectionProvider);
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public async Task<IActionResult> Index()
        {
            var UserId = Request.Cookies["UserId"];
            var res = await _adminViewModel.GetDataFromUserId(Convert.ToInt32(UserId));
            return View(res);
        }

        public async Task<IActionResult> ChangePassword()
        {
            var UserId = Request.Cookies["UserId"];
            var res = await _adminViewModel.GetDataFromUserId(Convert.ToInt32(UserId));
            return View(res);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateProfile(Admin admin)
        {
            var res = await _adminViewModel.Admin_Upsert(admin);
            return Json(res);
        }

        [HttpPut]
        public async Task<JsonResult> Admin_ChangePassword(Admin admin)
        {
            var res = await _adminViewModel.Admin_ChangePassword(admin);
            return Json(res);
        }

    }
}
