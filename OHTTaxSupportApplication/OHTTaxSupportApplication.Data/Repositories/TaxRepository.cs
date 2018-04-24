using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITaxRepository : IRepository<Tax>
    {                                                                                           
    }

    public class TaxRepository : RepositoryBase<Tax>, ITaxRepository
    {
        public TaxRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}