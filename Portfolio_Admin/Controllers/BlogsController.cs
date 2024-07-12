using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Admin.Repository;

namespace Portfolio_Admin.Controllers
{
    public class BlogsController : Controller
    {
        private IClientHelper _clientHelper;
        private BlogsViewModel _blogsViewModel;
        private AdminViewModel _adminViewModel;
        private readonly IDataProtector _protector;

        public BlogsController(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper;
            _blogsViewModel = new BlogsViewModel(_clientHelper, dataProtectionProvider);
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
        public async Task<JsonResult> AddBlog(Blogs blogs)
        {
            if (blogs.Blog_Image != null)
            {
                var controllerName = this.ControllerContext.ActionDescriptor.ControllerName;
                var res1 = await _blogsViewModel.UploadFileAsync(blogs.Blog_Image, controllerName);
                blogs.Blog_Image_String = res1;
            }
            else
            {
                // Retrieve UserId from cookie and convert to long
                var UserId = long.Parse(Request.Cookies["UserId"]);

                // Set DeletedBy field of the blog
                blogs.UpdatedBy = UserId;
            }

            var res = await _blogsViewModel.AddBlog(blogs);
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlog()
        {
            var followersModel = await _blogsViewModel.GetAllBlog();
            return Json(new { data = followersModel });
        }

        public async Task<IActionResult> AllBlogs()
        {
            return View();
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteBlog(Blogs blogs)
        {
            // Retrieve UserId from cookie and convert to long
            var UserId = long.Parse(Request.Cookies["UserId"]);

            // Set DeletedBy field of the blog
            blogs.DeletedBy = UserId;

            // Call async method to delete blog
            var res = await _blogsViewModel.DeleteBlog(blogs);

            // Return JSON result
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> EditBlogs(int id)
        {
            var followersModel = await _blogsViewModel.GetByIdBlog(id);
            return View(followersModel);
        }

    }
}
