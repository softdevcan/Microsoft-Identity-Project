using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Runtime.ExceptionServices;
using Microsoft_Identity_Project.Web.Extensions;
using Microsoft_Identity_Project.Core.ViewModels;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;
using Microsoft_Identity_Project.Service.Services;

namespace Microsoft_Identity_Project.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;
        private readonly IMemberService _memberService;
        private string userName => User.Identity!.Name!;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider, IMemberService memberService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await _memberService.GetUserViewModelByUserNameAsync(userName));
        }

        public async Task Logout()
        {
            await _memberService.LogoutAsync();
        }

        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            
            if (!await _memberService.CheckPasswordAsync(userName,request.PasswordOld))
            {
                ModelState.AddModelError(string.Empty, "Old Password is wrong!");
                return View();
            }

            var (isSucceed, errors) = await _memberService.ChangePasswordAsync(userName, request.PasswordOld, request.PasswordNew);
            if (!isSucceed)
            {
                ModelState.AddModelErrorList(errors!);
                return View();
            }
            
            TempData["SuccessMessage"] = "Your password has been changed successfully.";
            
            return View();
        }
    
        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genderList = _memberService.GetGenderSelectList();

            return View(await _memberService.GetUserEditViewModelAsync(userName));
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var (isSucceed, errors) = await _memberService.EditUserAsync(request, userName);

            if (!isSucceed) 
            {
                ModelState.AddModelErrorList(errors!);
                return View();
            }

            TempData["SuccessMessage"] = "Your profile information has been changed successfully.";

            return View(await _memberService.GetUserEditViewModelAsync(userName));
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.Message = "You are not authorized to view this page. Contact your administrator to obtain authorization.";

            return View();
        }
    }
}
