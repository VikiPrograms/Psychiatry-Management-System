﻿using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DbManager<T, K> where K : IConvertible
    {
        private readonly IDb<T, K> context;

        public DbManager(IDb<T, K> context)
        {
            this.context = context;
        }

        public async Task CreateAsync(T item)
        {
            await context.CreateAsync(item);
        }

        public async Task<T> ReadAsync(K key, bool useNavigationalProperties = false)
        {
            return await context.ReadAsync(key);
        }

        public async Task<ICollection<T>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            return await context.ReadAllAsync(useNavigationalProperties);
        }

        public async Task UpdateAsync(T item, bool useNavigationalProperties = false)
        {
            await context.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(K key)
        {
            await context.DeleteAsync(key);
        }
    }
}
