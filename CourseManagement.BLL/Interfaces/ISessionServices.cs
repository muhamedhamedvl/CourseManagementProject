using CourseManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Interfaces
{
    public interface ISessionServices
    {
        Task<IEnumerable<Session>> GetByCourseAsync(int courseId);
        Task<Session?> GetAsync(int id);
        Task<bool> CreateAsync(Session s);
        Task<bool> UpdateAsync(Session s);
        Task<bool> DeleteAsync(int id);
    }
}
