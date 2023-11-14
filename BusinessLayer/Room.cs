using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        [Range(0,5, ErrorMessage = "The rooms capacity can't be more that 5")]
        public int Capacity { get; set; }    
        public List<Patient> Patients { get; set; }//Functionality - to be able to add, remove and change the patients in the room

        public Room() 
        {
            Patients = new List<Patient>();
        }
        public Room(int roomId, int capacity)
        {
            RoomId = roomId;
            Capacity = capacity;
            Patients = new List<Patient>();
        }
    }
}
