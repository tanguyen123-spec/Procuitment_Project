using Produ_project.Enitity;
using Produ_project.Model;
using Produ_project.Repository;

namespace Produ_project.Service
{
    public interface ISuppierInfo
    {
        Task<IEnumerable<SupplierInFo>> GetAll();
        Task<SupplierInFo> GetById(string id);
        Task Create(SupplierInFo entity);
        Task CreatebyModels(SupplierInfoModel ctdatve);
        Task Update(string id, SupplierInFo entity);
        Task Delete(string id);
    }
    public class SupplierInfoService : ISuppierInfo
    {
        private readonly IRepository<SupplierInFo> _repository;
        private readonly Produ_projectContext _context;
        public SupplierInfoService(IRepository<SupplierInFo> repository, Produ_projectContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task Create(SupplierInFo entity)
        {
            await _repository.Create(entity);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<SupplierInFo>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SupplierInFo> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(string id, SupplierInFo entity)
        {
            await _repository.Update(id, entity);
        }
        public async Task CreatebyModels(SupplierInfoModel supplierInfo)
        {
            var SLI = new SupplierInFo
            {
                SlId = supplierInfo.SlId,
                SupplierName = supplierInfo.SupplierName,
                CategoriesId = supplierInfo.CategoriesId,
                Address = supplierInfo.Address,
                City = supplierInfo.City,
                EstablishedYear = supplierInfo.EstablishedYear,
                Numberofworkers= supplierInfo.Numberofworkers,
                MainProductId = supplierInfo.MainProductId,
                Moq = supplierInfo.Moq,
                Certificate = supplierInfo.Certificate,
                Customized = supplierInfo.Customized,
                SampleProcess = supplierInfo.SampleProcess,
                Leadtime = supplierInfo.Leadtime,
                ExportUs = supplierInfo.ExportUs,
                Websitelink = supplierInfo.Websitelink,
                Email = supplierInfo.Email,
                Phone = supplierInfo.Phone,
                ContactPerson = supplierInfo.ContactPerson,
                Note = supplierInfo.Note,   
                UserId = supplierInfo.UserId,
                ReviewQa = supplierInfo.ReviewQa,
                DateQa = supplierInfo.DateQa,
            };
            _context.SupplierInFos.Add(SLI);
            await _context.SaveChangesAsync();
        }

    }
}
