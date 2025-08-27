using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Entities;
using CourseManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _repo;
        public GradeService(IGradeRepository repo) => _repo = repo;
        public Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId)
        => _repo.GetBySessionAsync(sessionId);
        public Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId)
        => _repo.GetByTraineeAsync(traineeId);
        public Task<Grade?> GetAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<bool> CreateAsync(Grade g)
        {
            if (g.Value < 0 || g.Value > 100) return false;
            await _repo.AddAsync(g); return true;
        }
        public async Task<bool> UpdateAsync(Grade g)
        {
            if (g.Value < 0 || g.Value > 100) return false;
            await _repo.UpdateAsync(g); return true;
        }
        public async Task<bool> DeleteAsync(int id)
        { await _repo.DeleteAsync(id); return true; }
    }
}
