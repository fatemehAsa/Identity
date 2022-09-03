using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Interfaces
{
   public interface IUserService
    {
       List<string> GetAllUsers(string token);
    }
}
