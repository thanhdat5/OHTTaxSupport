using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ICustomerTypeRepository : IRepository<CustomerType>
    {                                                                                           
    }

    public class CustomerTypeRepository : RepositoryBase<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}