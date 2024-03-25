using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserContext userContext;

        public UsersController(UserContext context)
        {
            userContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await userContext.ReadAllUsersAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            User user = await userContext.ReadUserAsync(id, true);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            LoadNavigationalEntities();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, UserName, Password, Email, PhoneNumber, Role")] User user)
        {
            if (ModelState.IsValid)
            {
                await userContext.CreateUserAsync(user.UserName, user.Password, user.Email, user.PhoneNumber, user.Role);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await userContext.ReadUserAsync(id, true);
            LoadNavigationalEntities();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, UserName, Password, Email, PhoneNumber, Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await userContext.UpdateUserAsync(user.Id, user.UserName, user.Password, user.Email, user.Role, user.PhoneNumber);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            User user = await userContext.ReadUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            User user = await userContext.ReadUserAsync(id);
            if (user != null)
            {
                await userContext.DeleteUserByNameAsync(user.UserName);
            }
            return RedirectToAction(nameof(Index));
        }
        private void LoadNavigationalEntities()
        {
            ViewData["Role"] = new SelectList(Enum.GetValues(typeof(Role)));
        }
    }
}
