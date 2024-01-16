using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set;}
        public Occupation Occupation { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public Doctor()
        {
            Patients = new List<Patient>();
        }

        public Doctor(string name, Occupation occupation)
        {
            Name = name;
            Occupation = occupation;
            Patients = new List<Patient>();
        }
    }
}
