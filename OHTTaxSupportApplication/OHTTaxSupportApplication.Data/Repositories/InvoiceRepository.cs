using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {                                                                                           
    }

    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}