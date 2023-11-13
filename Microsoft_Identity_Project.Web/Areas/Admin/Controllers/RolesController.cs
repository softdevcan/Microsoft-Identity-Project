using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft_Identity_Project.Web.Areas.Admin.Models;
using Microsoft_Identity_Project.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;

namespace Microsoft_Identity_Project.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }


        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name!,
            }).ToListAsync();


            return View(roles);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });

            if(!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            TempData["SucceedMessage"] = "The role has been created successfully.";
            return RedirectToAction(nameof(RolesController.Index));
        }

        
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if(roleToUpdate == null)
            {
                throw new Exception("No roles to update were found!");
            }

            return View(new RoleUpdateViewModel() { Id= roleToUpdate.Id, Name = roleToUpdate!.Name!});
        }

        
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);

            if (roleToUpdate == null)
            {
                throw new Exception("No roles to update were found!");
            }
            roleToUpdate.Name = request.Name;

            await _roleManager.UpdateAsync(roleToUpdate);

            ViewData["SuccessMessage"] = "The role has been updated successfully.";

            return View();
        }

        
        public async Task<IActionResult> RoleDelete(string id)
        {
            var rolToDelete = await _roleManager.FindByIdAsync(id);
            if (rolToDelete == null)
            {
                throw new Exception("No role to delete was found!");
            }
            var result = await _roleManager.DeleteAsync(rolToDelete);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }
            TempData["SucceedMessage"] = "The role has been deleted successfully.";
            return RedirectToAction(nameof(RolesController.Index));
        }

        
        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = (await _userManager.FindByIdAsync(id))!;
            ViewBag.userId = id;
            var roles = await _roleManager.Roles.ToListAsync();

            var roleViewModelList = new List<AssignRoleToUserViewModel>();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            foreach (var role in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel() { Id = role.Id, Name = role.Name! };

                if(userRoles.Contains(role.Name!))
                {
                    assignRoleToUserViewModel.Exist = true;
                }

                roleViewModelList.Add(assignRoleToUserViewModel);
            }

            
            return View(roleViewModelList);
        }

        
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserViewModel> requestList)
        {
            var userToAssignRoles = (await _userManager.FindByIdAsync(userId))!;

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name);
                }
            }
            return RedirectToAction(nameof(HomeController.UserList), "Home");
        }
    }
}
