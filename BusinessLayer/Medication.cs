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
        [MaxLength(30, ErrorMessage = "Name cannot be more than 30 symbols!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(5000, ErrorMessage = "Description cannot be more than 5000 symbols!")]
        public string Description { get; set; }
        [Required]
        [Range(1,3000)]
        public int Quantity { get; set; }
        [Required]
        public decimal Cost { get; set; }

        public Medication() { }
        public Medication( string name, string description, int quantity, decimal cost)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Cost = cost;
        }
    }
}
