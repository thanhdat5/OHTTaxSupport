using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITaxValueRepository : IRepository<TaxValue>
    {                                                                                           
    }

    public class TaxValueRepository : RepositoryBase<TaxValue>, ITaxValueRepository
    {
        public TaxValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}