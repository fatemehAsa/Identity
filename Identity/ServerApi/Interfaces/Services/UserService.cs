using Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Interfaces.Services
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
            var result= await _context.AspNetUsers.Select(c => c.Email).ToListAsync();
            return result;
        }
    }
}
