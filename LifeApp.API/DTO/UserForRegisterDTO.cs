using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LifeApp.API.DTO
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }  

        [StringLength(8, MinimumLength =4, ErrorMessage ="Password length should between 4-8 characters")]
        public string Password { get; set; }  
    }
}