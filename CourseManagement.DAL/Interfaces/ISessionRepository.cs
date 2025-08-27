using CourseManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CourseManagement.DAL.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetByCourseAsync(int courseId);
        Task<Session?> GetByIdAsync(int id);
        Task AddAsync(Session session);
        Task UpdateAsync(Session session);
        Task DeleteAsync(int id);
    }
}
