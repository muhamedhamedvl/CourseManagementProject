using BLL.Pagination;
using BLL.ViewModels;
using DAL.BLL.Interfaces;
using DAL.Entitys;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IInstructorRepository _instructorRepo;

        public CourseService(ICourseRepository courseRepo, IInstructorRepository instructorRepo)
        {
            _courseRepo = courseRepo;
            _instructorRepo = instructorRepo;
        }

        public async Task<IEnumerable<BaseCourseVM>> GetAllAsync()
        {
            var courses = await _courseRepo.GetAllAsync();

            // Map Entity → ViewModel
            return courses.Select(c => new BaseCourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                //Category = c.Category,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                IsActive = c.IsActive,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor?.FirstName + " " + c.Instructor?.LastName
            });
        }

        public async Task<BaseCourseVM> GetByIdAsync(int id)
        {
            var c = await _courseRepo.GetByIdAsync(id);
            if (c == null) return null;

            return new BaseCourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                //Category = c.Category,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                IsActive = c.IsActive,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor?.FirstName + " " + c.Instructor?.LastName
            };
        }

        public async Task CreateAsync(CreateCourseVM model)
        {
            var entity = new Course
            {
                Name = model.Name,
                Description = model.Description,
                //Category = model.Category,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsActive = model.IsActive,
                //InstructorId = model.InstructorId
            };

            await _courseRepo.AddAsync(entity);
            await _courseRepo.SaveAsync();
        }

        public async Task UpdateAsync(EditCourseVM model)
        {
            var course = await _courseRepo.GetByIdAsync(model.Id);
            if (course == null) return;

            course.Name = model.Name;
            course.Description = model.Description;
            //course.Category = model.Category;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.IsActive = model.IsActive;
            //course.InstructorId = model.InstructorId;

            _courseRepo.Update(course);
            await _courseRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return;

            _courseRepo.Remove(course);
            await _courseRepo.SaveAsync();
        }

        public PagedResult<BaseCourseVM> GetPagedourses(int page, int pageSize, string? searchedName)
        {
            var totalcount = _courseRepo.GetCount(searchedName);
            var courses = _courseRepo.GetPage(page, pageSize, searchedName)
            .Select(c => new BaseCourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                IsActive = c.IsActive,
            }).ToList();
            return new PagedResult<BaseCourseVM>
            {
                Items = courses,
                TotalCount = totalcount,
                Pagesize = pageSize,
                Page = page
            };

            }
        

        //public async Task<bool> ExistsAsync(int id)
        //    => await _courseRepo.ExistsAsync(id);
    }
}
