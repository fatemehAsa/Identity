using Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Interfaces.Services
{
    public class UserService : IUserService
    {
        private CustomContext _context;
        public UserService(CustomContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetAllEmails()
        {
            var res =await _context.AspNetUsers.Select(c => c.Email).ToListAsync();
            return res;
        }
    }
}
