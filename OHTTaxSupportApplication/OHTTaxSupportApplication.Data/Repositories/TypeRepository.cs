using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITypeRepository : IRepository<Type>
    {                                                                                           
    }

    public class TypeRepository : RepositoryBase<Type>, ITypeRepository
    {
        public TypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}