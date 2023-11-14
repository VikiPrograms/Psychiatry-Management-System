using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Name cannot be more than 30 symbols!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Name cannot be more than 30 symbols!")]
        public string Description { get; set; }
        [Required]    
        public int Quantity { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public Treatment Treatment { get; set; }

        public Medication() { }
        public Medication( string name, string description, int quantity, decimal cost, Treatment treatment)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Cost = cost;
            Treatment = treatment;
        }
    }
}
