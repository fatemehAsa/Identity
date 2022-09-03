using Identity.Core.DTOs;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;

        public AuthController(
            UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager,
            RoleManager<AspNetRole> roleManager)
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                return NotFound("کاربری با مشخصات وارد شده یافت نشد");
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var tokenString = string.Empty;
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDFsf35rsdf$TGsdf344"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOption = new JwtSecurityToken(
                    issuer: "https://localhost:44326",
                    claims: new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name,
                         login.Email)
                    }, expires: DateTime.Now.AddMinutes(30), signingCredentials: signingCredentials);

                tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
               
                return Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest();
            }
        }




    }
}
