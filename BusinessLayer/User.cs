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

        public string Password { get; set; }
        public Role Role { get; set; }
        public List<Patient> Patients { get; set; }
        public User()
        {
            Patients = new List<Patient>();
        }

        public User(string username, string password, string email, Role role, string telephone = null)
        {
            UserName = username;
            Password = password;
            Email = email;
            Role = role;
            PhoneNumber = telephone;
            Patients = new();
        }
    }
}
