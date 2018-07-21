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
                    Value = decimal.Parse(m.Value.ToString()).ToString("###,##"),
                    IsActive = m.IsActive ?? false,
                    DepartmentID = m.DepartmentID,
                    CategoryID = m.CategoryID,
                    TaxValueID = m.TaxValueID,
                    TaxValue = DbContext.TaxValues.Find(m.TaxValueID).Value.ToString(),
                    Department = DbContext.Departments.Find(m.DepartmentID).DepartmentName,
                    Category = DbContext.Categories.Find(m.CategoryID).CategoryName
                }).ToList();
        }
    }
}