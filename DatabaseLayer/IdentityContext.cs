using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class IdentityContext//wont need it for now
    {
        UserManager<User> userManager;
        PsychiatryDbContext context;

        public IdentityContext(UserManager<User> userManager, PsychiatryDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        #region Seeding Data With This Project

        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            //await context.Database.MigrateAsync();

            int userRoles = await context.UserRoles.CountAsync();
            if(userRoles == 0)
            {
                await ConfigureAdminAccountAsync(adminPass, adminEmail);              
            }
        }

        public async Task ConfigureAdminAccountAsync(string password, string email)
        {
            User adminIdentityUser = await context.Users.FirstAsync();

            if(adminIdentityUser == null)
            {
                await userManager.AddToRoleAsync(adminIdentityUser, Role.Administrator.ToString());//tf should i do?!?
                await userManager.AddPasswordAsync(adminIdentityUser, password);
                await userManager.SetEmailAsync(adminIdentityUser, email);
            }
        }
        #endregion

        #region CRUD
        #endregion//for now it is empty 
    }
}
