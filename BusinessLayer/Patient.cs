using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateOnly? Checkout { get; set; }
        public Room Room { get; set; }

        public Patient()
        {
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now);
        }
        public Patient(string name, int age, DateOnly? checkout, Room room)//with checkout 
        {
            Name = Name;
            Age = age;
            Checkout = checkout;
            Room = room;
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now);
        }
        public Patient(string name, int age, Room room)//without checkout
        {
            Name = Name;
            Age = age;
            Room = room;
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
