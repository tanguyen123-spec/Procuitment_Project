using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Repository;

namespace Produ_project.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Getall();
        Task Create(User user);
        Task Update(string id, User user);
        Task Delete(string id);

        Task CreateByModels(UserModel user);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly Produ_projectContext _context;
        
        public UserService( IRepository<User> repository, Produ_projectContext context)
        {
            
            _repository = repository;
            _context = context;
        }
        public async Task Create(User user)
        {
            await _repository.Create(user);
        }

        public async Task CreateByModels(UserModel user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var User = new User
            {
                NameUser = user.NameUser,
                Password = passwordHash,
                Role = user.Role,
                UserId = user.UserId
            };
            _context.Add(User);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<User>> Getall()
        {
            return await _repository.GetAll();
        }

        public async Task Update(string id, User user)
        {
            await _repository.Update(id, user);
        }
    }
}
