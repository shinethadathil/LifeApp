using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }    
        public Byte[] PasswordHash { get; set; }    
        public Byte[] PasswordSalt { get; set; }
    }
}