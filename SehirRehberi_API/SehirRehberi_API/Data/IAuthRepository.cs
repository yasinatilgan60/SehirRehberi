using SehirRehberi_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi_API.Data
{
    public interface IAuthRepository
    {
        // Asenkron
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string passWord);
        Task<bool> UserExists(string userName);
    }
}
