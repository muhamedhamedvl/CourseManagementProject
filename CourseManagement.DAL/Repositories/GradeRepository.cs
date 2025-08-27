using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entities;
using CourseManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _ctx;
        public GradeRepository(AppDbContext ctx) => _ctx = ctx;
        public async Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId)
        => await _ctx.Grades.Where(g => g.SessionId == sessionId)
        .Include(g => g.Trainee)
        .OrderBy(g => g.Trainee!.Name)
        .ToListAsync();
        public async Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId)
        => await _ctx.Grades.Where(g => g.TraineeId == traineeId)
        .Include(g => g.Session)
        .ThenInclude(s => s!.Course)
        .ToListAsync();
        public Task<Grade?> GetByIdAsync(int id) => _ctx.Grades
        .Include(g => g.Trainee)
        .Include(g => g.Session)
        .FirstOrDefaultAsync(g => g.Id == id);
        public async Task AddAsync(Grade grade)
        {
            _ctx.Grades.Add(grade);
            await _ctx.SaveChangesAsync();
        }
        public async Task UpdateAsync(Grade grade)
        {
            _ctx.Grades.Update(grade);
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Grades.FindAsync(id);
            if (entity is null) return;
            _ctx.Grades.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }

}
