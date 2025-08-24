using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> GetAllAsync(string search, string role, int page, int pageSize) =>
            _userRepository.GetAllAsync(search, role, page, pageSize);

        public Task<User> GetByIdAsync(int id) =>
            _userRepository.GetByIdAsync(id);

        public Task AddAsync(User user) =>
            _userRepository.AddAsync(user);

        public Task UpdateAsync(User user) =>
            _userRepository.UpdateAsync(user);

        public Task DeleteAsync(int id) =>
            _userRepository.DeleteAsync(id);

        public Task<bool> EmailExistsAsync(string email, int? excludeUserId = null) =>
            _userRepository.EmailExistsAsync(email, excludeUserId);
    }
}
