using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {                                                                                           
    }

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}