using System;
using System.Drawing.Drawing2D;
using BusinessLayer;
using DatabaseLayer;

namespace TestingLayer 
{
    class Program//why tf does the async methods stop the Console.WriteLine from showing???
    {
        static PsychiatryDbContext dbContext;
        static DoctorContext doctorsContext;
        static MedicationContext medicationsContext;
        static PatientContext patientsContext;
        static RoomContext roomsContext;
        static void Main(string[] args)
        {
            try
            {
                dbContext = new PsychiatryDbContext();
                doctorsContext = new DoctorContext(dbContext); 
                medicationsContext = new MedicationContext(dbContext); 
                patientsContext = new PatientContext(dbContext); 
                roomsContext = new RoomContext(dbContext);

                TestDoctorContextCreate();
                TestDoctorContextRead();
                TestDoctorContextReadAll();
                TestDoctorContextUpdate();
                //System.InvalidOperationException: 'A second operation was started on this context instance before a previous operation completed.
                //This is usually caused by different threads concurrently using the same instance of DbContext. 
                //throw new InvalidOperationException(CoreStrings.ConcurrentMethodInvocation);
                TestDoctorContextDelete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " :(");
            }

        }

        static async void TestDoctorContextCreate()
        {
            Doctor doctor = new Doctor("Viktoria Urucheva", Occupation.MainDoctor);
            await doctorsContext.CreateAsync(doctor);
            Doctor doctor1 = new Doctor("Ralitsa Toteva", Occupation.Doctor);
            await doctorsContext.CreateAsync(doctor1);
            Console.WriteLine("Doctor added successfully :)");
        }
        
        static async void TestDoctorContextRead()
        {
            Doctor doctor1 = await doctorsContext.ReadAsync(1);
            Console.WriteLine(doctor1);
            Console.WriteLine("The information about the doctor was read successfully!");
        }

        static async void TestDoctorContextReadAll()
        {
            IEnumerable<Doctor> doctors = await doctorsContext.ReadAllAsync();

            foreach (Doctor doc in doctors)
            {
                Console.WriteLine(doc);
            }
        }

        static async void TestDoctorContextUpdate()
        {
            Doctor doctorFromDb = await doctorsContext.ReadAsync(1);
            Console.WriteLine("Before: ");
            Console.WriteLine(doctorFromDb);

            doctorFromDb.Name = "TOshko ot 11J";
            await doctorsContext.UpdateAsync(doctorFromDb);

            Doctor updatedDoctorFromDb = await doctorsContext.ReadAsync(1);
            Console.WriteLine("After: ");
            Console.WriteLine(updatedDoctorFromDb);

        }

        static async void TestDoctorContextDelete()
        {
            Console.Write("Id = ");
            int id = Convert.ToInt32(Console.ReadLine());
            Doctor doctorFromDb = await doctorsContext.ReadAsync(id);

            Console.WriteLine("Before: {0}", doctorFromDb);
            await doctorsContext.DeleteAsync(id);

            doctorFromDb = await doctorsContext.ReadAsync(id);

            if (doctorFromDb == null)
            {
                Console.WriteLine($"Brand with Id {id} deleted successfully!");
            }

        }

    }
}
