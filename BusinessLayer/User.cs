using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User//patientuser class
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //After g-n Iliev shows how to do this, i need to add - List<Patients> for the doc and lists for everything for the admin
        //If there is a nurse, add List<Rooms>
    }
}
