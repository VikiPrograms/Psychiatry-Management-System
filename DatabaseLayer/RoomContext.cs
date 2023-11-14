using BusinessLayer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class RoomContext : IDb<Room, int>
    {
        private readonly PsychiatryDbContext dbContext;

        public RoomContext(PsychiatryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Room item)
        {
            try
            {
                dbContext.Rooms.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public void Delete(int key)
        {
            Room roomFromDb = Read(key);
            dbContext.Rooms.Remove(roomFromDb);
            dbContext.SaveChanges();
        }

        public Room Read(int key, bool usenavigationalproperties = false)
        {
            try
            {
                return dbContext.Rooms.Find(key);
            }
            catch(Exception) { throw; }
        }

        public IEnumerable<Room> ReadAll(bool usenavigationalproperties = false)
        {
            try
            {
                return dbContext.Rooms.ToList();
            }
            catch (Exception) { throw; }
        }

        public void Update(Room item, bool usenavigationalproperties = false)
        {
            try
            {
                Room roomFromDb = Read(item.RoomId, usenavigationalproperties);
                if(roomFromDb != null)
                {
                    Create(item);
                    return;
                }

                roomFromDb.Capacity = item.Capacity;

                if (usenavigationalproperties)
                {
                    List<Patient> patients = new List<Patient>();

                    foreach (Patient p in dbContext.Patients)
                    {
                        Patient patientFromDb = dbContext.Patients.Find(p.PatientId);

                        if (patientFromDb != null)
                        {
                            patients.Add(patientFromDb);
                        }
                        else
                        {
                            patients.Add(p);
                        }

                    }

                    roomFromDb.Patients = patients;
                }

                dbContext.SaveChanges();

            }
            catch (Exception) { throw; }
        }
    }
}
