using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class DoctorContext : IDb<Doctor, int>
    {
        private PsychiatryDbContext dbContext;
        public DoctorContext(PsychiatryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Doctor item)
        {
            try
            {
                List<Patient> patients = new List<Patient>(item.Patients.Count);
                foreach(Patient pat in patients)
                {
                    Patient patientFromDb = await dbContext.Patients.FindAsync(pat.PatientId);

                    if(patientFromDb != null)
                    {
                        patients.Add(patientFromDb);
                    }
                    else
                    {
                        patients.Add(pat);
                    }

                }
                item.Patients = patients;
                dbContext.Doctors.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }   

        public async Task DeleteAsync(int key)
        {
            try
            {
                Doctor doctorFromDb = await ReadAsync(key, false, false);
                if(doctorFromDb != null)
                {
                    dbContext.Doctors.Remove(doctorFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Doctor with that id does not exist!");
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<ICollection<Doctor>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Doctor> query = dbContext.Doctors;
                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Patients);
                }
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<Doctor> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                using (var dbContext = new PsychiatryDbContext())
                {
                    IQueryable<Doctor> query = dbContext.Doctors;
                    if (useNavigationalProperties)
                    {
                        query = query.Include(o => o.Patients);
                    }
                    if (isReadOnly)
                    {
                        query = query.AsNoTrackingWithIdentityResolution();
                    }

                    return await query.FirstOrDefaultAsync(o => o.DoctorId == key);
                }

            }
            catch (Exception) { throw; }
        }

        public async Task UpdateAsync(Doctor item, bool useNavigationalProperties = false)
        {
            try
            {
                Doctor doctorFromDb = await ReadAsync(item.DoctorId, useNavigationalProperties, false);
                doctorFromDb.Name = item.Name;
                doctorFromDb.Occupation = item.Occupation;

                if (useNavigationalProperties)
                {
                    List<Patient> patients = new List<Patient>(item.Patients.Count);
                    foreach(var pat in patients)
                    {
                        Patient patientFromDb = await dbContext.Patients.FindAsync(pat.PatientId);
                        if(patientFromDb != null)
                        {
                            patients.Add(patientFromDb);
                        }
                        else
                        {
                            patients.Add(pat);
                        }
                    }
                    doctorFromDb.Patients = patients;
                }
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}
