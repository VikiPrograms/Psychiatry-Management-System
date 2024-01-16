using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Range(6, 140, ErrorMessage = "Age must be between 6 and 140!")]
        public int Age { get; set; }
       
        public User()
        {
        }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
