using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task CreateAsync(Patient item)
        {
            try
            {
                dbContext.Patients.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Patient patientFromDb = await ReadAsync(key, false, false);
                if(patientFromDb is null)
                {
                    throw new ArgumentException("This patient does not exist!");
                }

                dbContext.Patients.Remove(patientFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<ICollection<Patient>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Patient> query = dbContext.Patients;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.ToListAsync();

            }
            catch (Exception) { throw; }
        }

        public async Task<Patient> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Patient> query = dbContext.Patients;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(p => p.PatientId == key);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(Patient item, bool useNavigationalProperties = false)
        {
            try
            {
                Patient patientFromDb = await ReadAsync(item.PatientId, useNavigationalProperties, false);
                patientFromDb.Name = item.Name;
                patientFromDb.Age = item.Age;
                patientFromDb.Checkout = item.Checkout;
                patientFromDb.Room = item.Room;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}
