    using Produ_project.Enitity;
    using Produ_project.Model;
    using Produ_project.Repository;

    namespace Produ_project.Service
    {
        public interface IcategoryService
        {
            Task<IEnumerable<Category>> GetAll();
            Task<Category> GetById(string id);
            Task Create(Category entity);
            Task CreatebyModels(CategoryModel Category);
            Task Update(string id, Category entity);
            Task Delete(string id);
        }
        public class CategoriesService : IcategoryService
        {
            private readonly IRepository<Category> _repository;
            private readonly Produ_projectContext _context;
            public CategoriesService(IRepository<Category> repository, Produ_projectContext context)
            {
                _repository = repository;
                _context = context;
            }

            public async Task Create(Category entity)
            {
                await _repository.Create(entity);
            }

            public async Task Delete(string id)
            {
                await _repository.Delete(id);
            }

            public async Task<IEnumerable<Category>> GetAll()
            {
                return await _repository.GetAll();
            }

            public async Task<Category> GetById(string id)
            {
                return await _repository.GetById(id);
            }

            public async Task Update(string id, Category entity)
            {
                await _repository.Update(id, entity);
            }
            public async Task CreatebyModels(CategoryModel category)
            {
                var CG = new Category
                {
                    CategoriesId = category.CategoriesId,
                    NameCategories = category.NameCategories,
                };
                _context.Categories.Add(CG);
                await _context.SaveChangesAsync();
            }

        }
    }
