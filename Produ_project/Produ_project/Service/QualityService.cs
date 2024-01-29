using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Repository;

namespace Produ_project.Service
{
    public interface Iquality
    {
        Task<IEnumerable<Quality>> GetAll();
        Task<Quality> GetById(string id);
        Task Create(Quality entity);
        Task CreatebyModels(QualityModel ctdatve);
        Task Update(string id, Quality entity);
        Task Delete(string id);
    }
    public class QualityService : Iquality
    {
        private readonly IRepository<Quality> _repository;
        private readonly Produ_projectContext _context;
        public QualityService(IRepository<Quality> repository, Produ_projectContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task Create(Quality entity)
        {
            await _repository.Create(entity);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<Quality>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Quality> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(string id, Quality entity)
        {
            await _repository.Update(id, entity);
        }
        public async Task CreatebyModels(QualityModel quality)
        {
            var Q = new Quality
            {
                Awid = quality.Awid,
                Pcscustomer = quality.Pcscustomer,
                Color = quality.Color,
                Size = quality.Size,
                Note = quality.Note

            };
            _context.Qualities.Add(Q);
            await _context.SaveChangesAsync();
        }

    }
}
