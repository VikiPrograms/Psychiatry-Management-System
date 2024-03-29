﻿using BusinessLayer;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Room item)
        {
            try
            {
                List<Patient> patients = new List<Patient>(item.Patients.Count);
                foreach (Patient pat in patients)
                {
                    Patient patientFromDb = await dbContext.Patients.FindAsync(pat.PatientId);

                    if (patientFromDb != null)
                    {
                        patients.Add(patientFromDb);
                    }
                    else
                    {
                        patients.Add(pat);
                    }

                }
                item.Patients = patients;
                dbContext.Rooms.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Room roomFromDb = await ReadAsync(key, false, false);
                if (roomFromDb != null)
                {
                    dbContext.Rooms.Remove(roomFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("This room does not exist");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Room>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Room> query = dbContext.Rooms;
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Room> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Room> query = dbContext.Rooms;
                if (useNavigationalProperties)
                {
                    query = query.Include(o => o.Patients);
                }
                if(isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(r => r.RoomId == key);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Room item, bool useNavigationalProperties = false)
        {
            try
            {
                Room roomFromDb = await ReadAsync(item.RoomId, useNavigationalProperties, false);
                roomFromDb.Capacity = item.Capacity;

                if (useNavigationalProperties)
                {
                    List<Patient> patients = new List<Patient>(item.Patients.Count);
                    foreach(var pat in patients)
                    {
                        Patient patientFromDb = await dbContext.Patients.FindAsync(pat.PatientId);
                        if (patientFromDb != null)
                        {
                            patients.Add(patientFromDb);
                        }
                        else
                        {
                            patients.Add(pat);
                        }
                    }
                    roomFromDb.Patients = patients;
                }
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
