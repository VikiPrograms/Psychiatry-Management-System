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

        public void Create(Medication item)
        {
            try
            {
                dbContext.Medications.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public void Delete(int key)
        {
            try
            {
                Medication medicationFromdB = Read(key);
                if(medicationFromdB != null)
                {
                    dbContext.Medications.Remove(medicationFromdB);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Medication with that key does not exist!");
                }
            }
            catch (Exception) { throw; }
        }

        public Medication Read(int key, bool usenavigationalproperties = false)
        {
            try
            {
                    return dbContext.Medications.Find(key);
            }
            catch(Exception) { throw; }
        }

        public IEnumerable<Medication> ReadAll(bool usenavigationalproperties = false)
        {
            try
            {
                IQueryable<Medication> query = dbContext.Medications;
                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Medication item, bool usenavigationalproperties = false)
        {
            try
            {
                Medication medicationFromDb = Read(item.MedicationId, usenavigationalproperties);
                if(medicationFromDb == null)
                {
                    Create(item);
                    return;
                }
                medicationFromDb.Name= item.Name;
                medicationFromDb.Description= item.Description;
                medicationFromDb.Treatment = item.Treatment;
                medicationFromDb.Cost = item.Cost;
                medicationFromDb.Quantity = item.Quantity;

                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }
    }
}
