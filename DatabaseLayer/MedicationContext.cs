using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class MedicationContext : IDb<Medication, int>
    {
        private readonly PsychiatryDbContext dbContext;
        public MedicationContext(PsychiatryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Medication item)
        {
            try
            {
                dbContext.Medications.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception) { throw; }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Medication medicationFromDb = await ReadAsync(key, false, false);

                if(medicationFromDb != null)
                {
                    dbContext.Medications.Remove(medicationFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This medication does not exist!");
                }
            }
            catch (Exception) { throw; }

        }

        public async Task<ICollection<Medication>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Medication> query = dbContext.Medications;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception) { throw; }

        }

        public async Task<Medication> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Medication> query = dbContext.Medications;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(m => m.MedicationId == key);
            }
            catch (Exception) { throw; }

        }

        public async Task UpdateAsync(Medication item, bool useNavigationalProperties = false)
        {
            try
            {
                Medication medicationFromDb = await ReadAsync(item.MedicationId, useNavigationalProperties, false);
                medicationFromDb.Cost = item.Cost;
                medicationFromDb.Quantity = item.Quantity;
                medicationFromDb.Name = item.Name;
                medicationFromDb.Description = item.Description;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }

        }
    }
}
