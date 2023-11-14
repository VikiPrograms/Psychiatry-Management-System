using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public string Name { get; set; }
        [Required, Range(0, int.MaxValue)]
        public string Description { get; set; }
        public List<Medication> Medications { get; set; }//Functionality - to be able to add, remove, change the medications and choose how much quantity to add to the treatement 

        /*public decimal SumMedication()
        {
            decimal totalCost = 0;
            foreach (var medication in Medications)
            {
                totalCost += medication.Cost * medication.Quantity;
            }
            return totalCost;
        }*/

        public Treatment()
        {
            Medications = new List<Medication>();
        }
        public Treatment(string name, string description)
        {         
            Name = name;
            Description = description;
            Medications = new List<Medication>();
        }
    }
}
