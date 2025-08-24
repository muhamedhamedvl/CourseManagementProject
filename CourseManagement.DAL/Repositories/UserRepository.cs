using CourseManagement.DAL.Data;
using CourseManagement.DAL.Enums;
using CourseManagement.DAL.Helpers.Pagenation;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string search, string role, int page, int pageSize)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.Name.Contains(search));

            if (!string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, out var parsedRole))
            {
                query = query.Where(u => u.Role == parsedRole);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && (!excludeUserId.HasValue || u.Id != excludeUserId));
        }
    }
}
