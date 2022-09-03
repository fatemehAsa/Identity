using Identity.Core.DTOs;
using Identity.Data.Entities;
using Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager,
            RoleManager<AspNetRole> roleManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = new AspNetUser { UserName = register.Email, Email = register.Email };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {

                return RedirectToAction("Login");
            }
            else
            {
                var errors = result.Errors.Select(c => c.Description);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("Password", error);
                }
            }
            return View(register);
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        private string CreateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = string.Empty;
            var key = Encoding.ASCII.GetBytes("SDFsf35rsdf$TGsdf344");
            var claims = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, email) });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = "")
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = CreateToken(login.Email);
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            }

            return View(login);
        }




        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {

                return View(resetPassword);
            }
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var errors = result.Errors.Select(c => c.Description);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("Password", error);
                }
            }

            return View(resetPassword);
        }
    }
}