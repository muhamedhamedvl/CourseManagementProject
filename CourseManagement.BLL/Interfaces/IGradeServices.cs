using CourseManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId);
        Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId);
        Task<Grade?> GetAsync(int id);
        Task<bool> CreateAsync(Grade g);
        Task<bool> UpdateAsync(Grade g);
        Task<bool> DeleteAsync(int id);
    }
}
