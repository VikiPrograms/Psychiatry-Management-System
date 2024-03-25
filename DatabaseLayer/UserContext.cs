using BusinessLayer;
using DatabaseLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext
    {
        UserManager<User> userManager;
        private readonly PsychiatryDbContext dbContext;

        public UserContext(PsychiatryDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        #region CRUD
        public async Task<IdentityResultSet<User>> CreateUserAsync(string username, string password, string email, string telephone, Role role)
        {
            try
            {
                User user = new User(username, password, email, role, telephone);
                IdentityResult result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return new IdentityResultSet<User>(result, user);
                }

                if (role == Role.Administrator)
                {
                    await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                }

                else
                {
                    await userManager.AddToRoleAsync(user, Role.User.ToString());
                }

                return new IdentityResultSet<User>(IdentityResult.Success, user);

            }
            catch (Exception ex)
            {
                IdentityResult identityResult = IdentityResult.Failed(new IdentityError()
                { Code = "Registration", Description = ex.Message });

                return new IdentityResultSet<User>(identityResult, null);
            }
        }

        public async Task<User> LogInUserAsync(string username, string password)
        {
            try
            {
                User user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);

                if (result.Succeeded)
                {
                    return await dbContext.Users.FindAsync(user.Id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> ReadUserAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                if (!useNavigationalProperties)
                {
                    return await userManager.FindByIdAsync(key);
                }
                else
                {
                    return await dbContext.Users.Include(u => u.Patients).SingleOrDefaultAsync(u => u.Id == key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAllUsersAsync(bool useNavigationalProperties = false)
        {
            try
            {
                if (!useNavigationalProperties)
                {
                    return await dbContext.Users.ToListAsync();
                }

                return await dbContext.Users.Include(u => u.Patients).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUserAsync(string id, string username, string password, string email, Role role, string telephone)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    User user = await dbContext.Users.FindAsync(id);
                    user.UserName = username;
                    user.PhoneNumber = telephone;
                    user.Email = email;
                    user.Password = password;
                    user.Role = role;

                    await userManager.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUserByNameAsync(string name)
        {
            try
            {
                User user = await FindUserByNameAsync(name);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> FindUserByNameAsync(string name)
        {
            try
            {
                return await userManager.FindByNameAsync(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion       
        public bool IsUserAdmin(User user)
        {
            return user != null ? user.Role == Role.Administrator : false;
        }
    }
}
