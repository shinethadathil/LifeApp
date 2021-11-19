using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeApp.API.Models;

namespace LifeApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login (string userId, string password);
        Task<bool> UserExists(string username);
    }
}