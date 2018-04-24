using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITaxCategoryRepository : IRepository<TaxCategory>
    {                                                                                           
    }

    public class TaxCategoryRepository : RepositoryBase<TaxCategory>, ITaxCategoryRepository
    {
        public TaxCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}