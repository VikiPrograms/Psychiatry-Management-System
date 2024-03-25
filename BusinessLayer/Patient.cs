using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Patient
    {
        [Key]
        [Required]
        public int PatientId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required]
        [Range(10,140, ErrorMessage ="Age must be between 10 and 140!")]
        public int Age { get; set; }
        [Required]
        public DateOnly AdmissionDate { get; set; }
        public Room Room { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        public Patient()
        {
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now);
        }
        public Patient(string name, int age, User? user = null)//with checkout 
        {
            Name = Name;
            Age = age;
            //Room = room;
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now);
            User = user;
        }
    }
}
