using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface IInvoiceDetailRepository : IRepository<InvoiceDetail>
    {
        IEnumerable<InvoiceDetailViewModel> GetListInvoiceDetailsByInvoiceID(int invoiceID);
    }

    public class InvoiceDetailRepository : RepositoryBase<InvoiceDetail>, IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<InvoiceDetailViewModel> GetListInvoiceDetailsByInvoiceID(int invoiceID)
        {
            return DbContext.InvoiceDetails
                .Where(m => m.InvoiceID == invoiceID && m.IsActive == true)
                .Select(m => new InvoiceDetailViewModel
                {
                    ID = m.ID,
                    InvoiceID = m.InvoiceID,
                    ProductID = m.ProductID,
                    UnitID = m.UnitID,
                    Value = m.Value,
                    Quanlity = m.Quanlity,
                    IsActive = m.IsActive ?? false,
                    Product = m.Product.ProductName,
                    Unit = m.Unit.Value
                }).ToList();
        }
    }
}