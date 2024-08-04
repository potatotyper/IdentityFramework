using Microsoft.AspNetCore.Mvc;
using IdentityFramework.ViewModels;
using Microsoft.AspNetCore.Identity;
using IdentityFramework.Models;
using IdentityFramework.Data;
using System;
using System.Collections.Generic;

namespace IdentityFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // by default its httpget unlesss specified by []
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel) ;
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again.";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginViewModel);
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            string password = registerViewModel.Password;
            bool symbol = false;
            bool number = false;
            bool lower = false;
            bool upper = false;
            foreach(var i in password)
            {
                if (!Char.IsLetterOrDigit(i))
                {
                    symbol = true;
                }
                else
                {
                    if(Char.IsDigit(i))
                    {
                        number = true;

                    }
                    if (Char.IsUpper(i))
                    {
                        upper = true;
                    }
                    if (Char.IsLower(i))
                    {
                        lower = true;
                    }
                }
            }
            var sumreqarr = new Dictionary<string, bool>()
            {
                {"symbol", symbol},
                {"number", number},
                {"lowercase letter", lower},
                {"uppercase letter", upper},
            };


            int sumreq = 0;
            foreach (var i in sumreqarr)
            {
                if (i.Value == false)
                {
                    sumreq++;
                }
            }
            if (sumreq != 0)
            {

                if (sumreq == 1)
                {
                    foreach (var i in sumreqarr)
                    {
                        if (i.Value == false)
                        {
                            if (i.Key == "uppercase letter")
                            {
                                TempData["Error"] = "The password requires an " + i.Key;
                            }
                            else
                            {
                                TempData["Error"] = "The passwrod requires a" + i.Key;
                            }
                        }
                    }
                }
                List<string> errorlist = new List<string>();
                if (sumreq == 2)
                {
                    foreach (var i in sumreqarr)
                    {
                        if (i.Value == false)
                        {
                            errorlist.Add(i.Key);
                        }
                    }
                    TempData["Error"] = "The passwrod requires a " + errorlist[0] + " and " + errorlist[1];
                }
                if (sumreq == 3)
                {
                    foreach (var i in sumreqarr)
                    {
                        if (i.Value == false)
                        {
                            errorlist.Add(i.Key);
                        }
                    }
                    TempData["Error"] = "The passwrod requires a " + errorlist[0] + ", " + errorlist[1] + " and " + errorlist[2];
                }
                return View(registerViewModel);
            }

            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                EmailConfirmed = true
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Index", "Race");
            }
            else
            {
                TempData["Error"] = "Account creation failed.";
                return View(registerViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }
    }
}
