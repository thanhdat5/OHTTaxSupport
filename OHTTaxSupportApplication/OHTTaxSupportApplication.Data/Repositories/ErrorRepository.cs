using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}