using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Web.Controllers
{
    public class CoursesController : Controller
    {
      

        private readonly ICourseService _courseService;
        private readonly AppDbContext _context; 
        private const int PageSize = 5;

        public CoursesController(ICourseService courseService, AppDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? category, int page = 1)
        {
            var total = await _courseService.GetCoursesCountAsync(search, category);
            var courses = await _courseService.GetPagedCoursesAsync(search, category, page, PageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / PageSize);
            ViewBag.Search = search;
            ViewBag.Category = category;

            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Instructors = await _context.Instructors.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!await _courseService.IsCourseNameUniqueAsync(course.Name))
                ModelState.AddModelError("Name", "Course name must be unique");

            if (ModelState.IsValid)
            {
                await _courseService.AddCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Instructors = await _context.Instructors.ToListAsync();
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null) return NotFound();
            // ✅ Populate ViewBag.Instructors
            var instructors = await _context.Instructors
                .Select(i => new
                {
                    i.Id,
                    FullName = i.FirstName + " " + i.LastName
                })
                .ToListAsync();
            ViewBag.Instructors = await _context.Instructors.ToListAsync();

            return View(course);


            //ViewBag.Instructors = await _context.Instructors.ToListAsync();
            //return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id) return NotFound();

            if (!await _courseService.IsCourseNameUniqueAsync(course.Name, course.Id))
                ModelState.AddModelError("Name", "Course name must be unique");

            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }
            if (!await _courseService.IsCourseNameUniqueAsync(course.Name, course.Id))
                ModelState.AddModelError("Name", "Course name must be unique");

            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }

            // ✅ Re-populate ViewBag.Instructors in case of form error
            var instructors = await _context.Instructors
                .Select(i => new
                {
                    i.Id,
                    FullName = i.FirstName + " " + i.LastName
                })
                .ToListAsync();

            ViewBag.Instructors = await _context.Instructors.ToListAsync();

            return View(course);


            //ViewBag.Instructors = await _context.Instructors.ToListAsync();
            //return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComplete(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }




}

