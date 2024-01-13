using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Patient//patientuser class???
    {
        [Key]
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }
        public DateTime? Checkout { get; set; }
        public Room Room { get; set; }
        public Treatment Treatment { get; set; }//for the doctor

        public Patient() { }
        public Patient(string firstName, string lastName, int age, DateTime admissionDate, DateTime? checkout, Room room, Treatment treatment)
        {         
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            AdmissionDate = admissionDate;
            Checkout = checkout;
            Room = room;
            Treatment = treatment;
        }
    }
}
