using CourseManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId);
        Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId);
        Task<Grade?> GetByIdAsync(int id);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(int id);
    }
}
