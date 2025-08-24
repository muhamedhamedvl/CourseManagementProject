using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string search, string role, int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllAsync(search, role, page, pageSize);
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.EmailExistsAsync(user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(user);
                }

                await _userService.AddAsync(user);
                TempData["SuccessMessage"] = "User created successfully ✅";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.EmailExistsAsync(user.Email, user.Id))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(user);
                }

                await _userService.UpdateAsync(user);
                TempData["SuccessMessage"] = "User updated successfully ✏️";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteAsync(id);
            TempData["SuccessMessage"] = "User deleted successfully 🗑️";
            return RedirectToAction(nameof(Index));
        }
    }
}
