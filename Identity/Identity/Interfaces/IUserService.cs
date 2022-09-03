using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Interfaces
{
   public interface IUserService
    {
        Task<List<string>> GetAllEmails();
    }
}
