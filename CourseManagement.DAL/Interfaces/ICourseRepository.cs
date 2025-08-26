
using DAL.Entitys;


namespace DAL.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course entity);
        void Update(Course entity);
        void Remove(Course entity);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
        Task<int> SaveAsync(); // Save() for Course
        int GetCount(string? searchNane = null);
        IEnumerable<Course> GetPage(int page, int pageSize, string? SearchName = null);
    }
}