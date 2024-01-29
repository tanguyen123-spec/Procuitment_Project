using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Repository;

namespace Produ_project.Service
{
    public interface IArtWorkService
    {
        Task<IEnumerable<ArtWork>> GetAll();
        Task<ArtWork> GetById(string id);
        Task Create(ArtWork entity);
        Task CreatebyModels(ArtWorkModel ctdatve);
        Task Update(string id, ArtWork entity);
        Task Delete(string id);
    }
    public class ArtWorkService : IArtWorkService
    {
        private readonly IRepository<ArtWork> _repository;
        private readonly Produ_projectContext _context;
        public ArtWorkService(IRepository<ArtWork> repository, Produ_projectContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task Create(ArtWork entity)
        {
            await _repository.Create(entity);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<ArtWork>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ArtWork> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(string id, ArtWork entity)
        {
            await _repository.Update(id, entity);
        }
        public async Task CreatebyModels(ArtWorkModel artWork)
        {
            var AW = new ArtWork
            {
                Awid = artWork.Awid,
                NameAw = artWork.NameAw,
                MainProductId = artWork.MainProductId,
                ImgagesUrl = artWork.ImgagesUrl
            };
            _context.ArtWorks.Add(AW);
            await _context.SaveChangesAsync();
        }

    }
}
