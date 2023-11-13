using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Core.ViewModels;
using Microsoft_Identity_Project.Repository.Models;
using Microsoft_Identity_Project.Web.Extensions;
using Microsoft_Identity_Project.Service.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;


namespace Microsoft_Identity_Project.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            var value = TempData["Email"];
            ViewBag.v = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmMailViewModel confirmMailViewModel)
        {
            var user = (await _userManager.FindByEmailAsync(confirmMailViewModel.Mail))!;
            if(user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("UserEdit","Member");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl ??= Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(model.Email);

            if(hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email or passworod is wrong!");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl!);
            }

            if(signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "You cannot log in for 3 minutes!" });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { $"Email or passworod is wrong!" });
            ModelState.AddModelErrorList(new List<string>() { $"Number of failed logins= {await _userManager.GetAccessFailedCountAsync(hasUser)}" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }
            Random rand = new Random();
            int confirmCode = rand.Next(100000, 1000000);
            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email, ConfirmCode = confirmCode}, request.PasswordConfirm);

            if (identityResult.Succeeded){
                await _emailService.SendConfirmEmail(confirmCode, request.Email!);
                TempData["Email"] = request.Email;
                TempData["SuccessMessage"] = "Congratulations! Your registration was successful. Please enter your missing information!";
                return RedirectToAction(nameof(HomeController.ConfirmEmail));
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());
            
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "No user found for this email address.");
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink=Url.Action("ResetPassword", "Home", new {userId = hasUser.Id, Token = passwordResetToken
                }, HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);

            TempData["SuccessMessage"] = $"We sent an email to {model.Email} with instructions to reset your password. If you do not receive a password reset message after 1 minute, verify that you entered the correct email address, or check your spam folder. If you need further assistance, ";
            return RedirectToAction(nameof(ForgotPassword));
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId; 
            TempData["token"] = token;

            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            if (userId == null || token==null)
            {
                throw new Exception("An error occurred!");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);
            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty,"User not found!");
                return View();
            }

            var result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your password has been successfully reset.";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x=>x.Description).ToList());
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}