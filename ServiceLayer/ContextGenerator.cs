using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ContextGenerator
    {
        private static PsychiatryDbContext dbContext;
        private static MedicationContext medicationContext;
        private static PatientContext patientContext;
        private static RoomContext roomContext;

        public static PsychiatryDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                SetDbContext();
            }
            return dbContext;
        }
        public static void SetDbContext()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }

            dbContext = new PsychiatryDbContext();
        }

        public static MedicationContext GetMedicationContext()
        {
            if (medicationContext == null)
            {
                SetMedicationContext();
            }
            return medicationContext;
        }

        public static void SetMedicationContext()
        {
            medicationContext = new MedicationContext(GetDbContext());
        }

        public static PatientContext GetPatientContext()
        {
            if (patientContext == null)
            {
                SetPatientContext();
            }
            return patientContext;
        }

        public static void SetPatientContext()
        {
            patientContext = new PatientContext(GetDbContext());
        }

        public static RoomContext GetRoomContext()
        {
            if (roomContext == null)
            {
                SetRoomContext();
            }
            return roomContext;
        }
        public static void SetRoomContext()
        {
            roomContext = new RoomContext(GetDbContext());
        }
    }
}
