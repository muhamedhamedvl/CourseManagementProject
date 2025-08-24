using CourseManagement.DAL.Helpers.Pagenation;
using CourseManagement.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<User>> GetAllAsync(string search, string role, int page, int pageSize);
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);
    }
}
