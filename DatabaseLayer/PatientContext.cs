using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class PatientContext : IDb<Patient, int>
    {
        private readonly PsychiatryDbContext dbContext;

        public PatientContext(PsychiatryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Patient item)
        {
            try
            {
                dbContext.Patients.Add(item);
                dbContext.SaveChanges();
            }
            catch(Exception) { throw; }
        }

        public void Delete(int key)
        {
            try
            {
                Patient patientFromDb = Read(key);
                dbContext.Patients.Remove(patientFromDb);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public Patient Read(int key, bool usenavigationalproperties = false)
        {
            try
            {
                return dbContext.Patients.Find(key);
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<Patient> ReadAll(bool usenavigationalproperties = false)
        {
            try
            {
                IQueryable<Patient> query = dbContext.Patients;
                return query.ToList();
            }
            catch (Exception) { throw; }
        }

        public void Update(Patient item, bool usenavigationalproperties = false)
        {
            try
            {
                Patient patientFromDb = Read(item.PatientId, usenavigationalproperties);
                if(patientFromDb != null)
                {
                    Create(item);
                    dbContext.SaveChanges();
                }
                else
                {
                    patientFromDb.FirstName=item.FirstName;
                    patientFromDb.LastName=item.LastName;
                    patientFromDb.Treatment=item.Treatment;
                    patientFromDb.AdmissionDate=item.AdmissionDate;
                    patientFromDb.Checkout=item.Checkout;
                    patientFromDb.Room=item.Room;
                    patientFromDb.Age=item.Age;
                }
            }
            catch (Exception) { throw; }
        }
    }
}
