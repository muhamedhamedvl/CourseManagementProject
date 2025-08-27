using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entities;
using CourseManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DAL.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _ctx;
        public SessionRepository(AppDbContext ctx) => _ctx = ctx;
        public async Task<IEnumerable<Session>> GetByCourseAsync(int courseId)
        => await _ctx.Sessions.Where(s => s.CourseId == courseId)
        .OrderBy(s => s.StartDate)
        .ToListAsync();
        public Task<Session?> GetByIdAsync(int id) => _ctx.Sessions
        .Include(s => s.Course)
        .FirstOrDefaultAsync(s => s.Id == id);
        public async Task AddAsync(Session session)
        {
            _ctx.Sessions.Add(session);
            await _ctx.SaveChangesAsync();
        }
        public async Task UpdateAsync(Session session)
        {
            _ctx.Sessions.Update(session);
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Sessions.FindAsync(id);
            if (entity is null) return;
            _ctx.Sessions.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}