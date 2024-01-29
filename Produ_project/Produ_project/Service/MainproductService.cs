using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Repository;

namespace Produ_project.Service
{
    public interface ImainProduct
    {
        Task<IEnumerable<MainProduct>> GetAll();
        Task<MainProduct> GetById(string id);
        Task Create(MainProduct entity);
        Task CreatebyModels(MainProductModel ctdatve);
        Task Update(string id, MainProduct entity);
        Task Delete(string id);
    }
    public class MainproductService : ImainProduct
    {
        private readonly IRepository<MainProduct> _repository;
        private readonly Produ_projectContext _context;
        public MainproductService(IRepository<MainProduct> repository, Produ_projectContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task Create(MainProduct entity)
        {
            await _repository.Create(entity);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<MainProduct>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<MainProduct> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(string id, MainProduct entity)
        {
            await _repository.Update(id, entity);
        }
        public async Task CreatebyModels(MainProductModel mainProduct)
        {
            var MP = new MainProduct
            {
                MainProductId = mainProduct.MainProductId,
                NameMp = mainProduct.NameMp,
                CategoriesId = mainProduct.CategoriesId,
               
            };
            _context.MainProducts.Add(MP);
            await _context.SaveChangesAsync();
        }

    }
}
