using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Interfaces.Services
{
    public class UserService : IUserService
    {
        private string apiUrl = "https://localhost:44326/Api/Customer";
        private HttpClient _client;
        public UserService()
        {
            _client = new HttpClient();
        }
        public List<string> GetAllUsers(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(apiUrl).Result;
            List<string> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result);
            return list;
        }

       
    }
}
