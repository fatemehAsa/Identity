using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CustomerController : ControllerBase
    {
        private IUserService _userService;
        public CustomerController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmail()
        {
            var emails =await _userService.GetAllEmails();
            return Ok(emails);
        }
    }
}
