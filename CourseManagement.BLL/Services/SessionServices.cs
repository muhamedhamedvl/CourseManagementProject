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
    public class SessionServices : ISessionServices
    {
        private readonly ISessionRepository _repo;
        public SessionServices(ISessionRepository repo) => _repo = repo;
        public Task<IEnumerable<Session>> GetByCourseAsync(int courseId)
        => _repo.GetByCourseAsync(courseId);
        public Task<Session?> GetAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<bool> CreateAsync(Session s)
        {
            if (s.StartDate.Date < DateTime.Today) return false;
            if (s.EndDate <= s.StartDate) return false;
            await _repo.AddAsync(s); return true;
        }

        public Task<bool> UpdateAsync(Session s)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

