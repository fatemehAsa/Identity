
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Interfaces
{
    public interface IUserService
    {
        Task<List<string>> GetAllEmails();
    }
}
