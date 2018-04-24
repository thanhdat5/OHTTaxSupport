using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITaxTypeRepository : IRepository<TaxType>
    {                                                                                           
    }

    public class TaxTypeRepository : RepositoryBase<TaxType>, ITaxTypeRepository
    {
        public TaxTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}