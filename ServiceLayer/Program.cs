using System.Data;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLayer;

namespace SeedingLayer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                IdentityOptions options = new IdentityOptions();
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer(
                    "Server=PC\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;"
                    );

                PsychiatryDbContext dbContext = new PsychiatryDbContext(builder.Options);
                UserManager<User> userManager = new UserManager<User>(
                    new UserStore<User>(dbContext), Options.Create(options),
                    new PasswordHasher<User>(), new List<IUserValidator<User>>() { new UserValidator<User>() },
                    new List<IPasswordValidator<User>>() { new PasswordValidator<User>() }, new UpperInvariantLookupNormalizer(),
                    new IdentityErrorDescriber(), new ServiceCollection().BuildServiceProvider(),
                    new Logger<UserManager<User>>(new LoggerFactory())
                    );

                UserContext identityContext = new UserContext(dbContext, userManager);

                dbContext.Roles.Add(new IdentityRole("Administrator") { NormalizedName = "ADMINISTRATOR" });
                dbContext.Roles.Add(new IdentityRole("User") { NormalizedName = "USER" });
                await dbContext.SaveChangesAsync();

                IdentityResultSet<User> result = await identityContext.CreateUserAsync("admin", "admin", "admincho@abv.bg", "0894928332", Role.Administrator);

                Console.WriteLine("Roles added successfully!");

                if (result.IdentityResult.Succeeded)
                {
                    Console.WriteLine("Admin account added successfully!");
                }
                else
                {
                    Console.WriteLine("Admin account failed to be created!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }
    }
}