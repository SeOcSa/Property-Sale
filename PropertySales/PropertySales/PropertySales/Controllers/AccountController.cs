using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertySales.ViewModels;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace PropertySales.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //register(GET + POST)

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createResult = await _userManager.CreateAsync(
                     new IdentityUser
                     {
                         Email = model.Email,
                         UserName = model.Email
                     },
                     model.Password);

                if (createResult.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(createdUser, false);

                    var adminRole = await _roleManager.FindByNameAsync("Admin");

                    var userRole = await _roleManager.FindByNameAsync("User");

                    if(userRole == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                        userRole = await _roleManager.FindByNameAsync("User");
                    }

                    await _userManager.AddToRoleAsync(createdUser, "User");

                    if (adminRole == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        adminRole = await _roleManager.FindByNameAsync("Admin");

                        await _userManager.AddToRoleAsync(createdUser, "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var allErrors = string.Join(" ### ",
                        createResult.Errors.Select(x => x.Description));
                    ModelState.AddModelError("", allErrors);
                }
            }

            return View(model);
        }
        //login(GET + POST)
        [HttpGet]
        public IActionResult Login(String ReturnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Could not log in");
                }
            }


            return View();
        }

        //logout
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        public string AccessDenied()
        {
            return "You are not authorized to perform this action!";
        }
    }
}
    
