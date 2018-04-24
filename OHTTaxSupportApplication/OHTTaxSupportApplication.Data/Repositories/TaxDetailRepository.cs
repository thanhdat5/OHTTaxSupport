using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;

namespace OHTTaxSupportApplication.Data.Repositories
{
    public interface ITaxDetailRepository : IRepository<TaxDetail>
    {
        IEnumerable<TaxDetailViewModel> GetListTaxDetailsByTaxID(int taxId);
    }

    public class TaxDetailRepository : RepositoryBase<TaxDetail>, ITaxDetailRepository
    {
        public TaxDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<TaxDetailViewModel> GetListTaxDetailsByTaxID(int taxId)
        {
            return DbContext.TaxDetails
                .Where(m => m.TaxID == taxId && m.IsActive == true)
                .Select(m => new TaxDetailViewModel
                {
                    ID = m.ID,
                    TaxID = m.TaxID,
                    ProductID = m.ProductID,
                    UnitID = m.UnitID,
                    Value = m.Value,
                    Quanlity = m.Quanlity,
                    Note = m.Note,
                    IsActive = m.IsActive ?? false,
                    Product = m.Product.ProductName,
                    Unit = m.Unit.Value
                }).ToList();
        }
    }
}