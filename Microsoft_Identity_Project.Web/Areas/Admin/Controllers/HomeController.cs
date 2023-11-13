using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;
using Microsoft_Identity_Project.Web.Areas.Admin.Models;

namespace Microsoft_Identity_Project.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x=> new UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName,

            }).ToList();

            return View(userViewModelList);
        }
    }
}
