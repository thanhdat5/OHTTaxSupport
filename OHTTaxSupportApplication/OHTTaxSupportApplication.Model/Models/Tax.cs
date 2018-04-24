using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Tax
    {
        public Tax()
        {
            this.TaxDetails = new List<TaxDetail>();
        }

        public int ID { get; set; }
        public Nullable<int> TaxTypeID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public int TaxValueID { get; set; }
        public int DepartmentID { get; set; }
        public int TaxCategoryID { get; set; }
        public bool InOut { get; set; }
        public int Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual TaxCategory TaxCategory { get; set; }
        public virtual TaxType TaxType { get; set; }
        public virtual TaxValue TaxValue { get; set; }
        public virtual ICollection<TaxDetail> TaxDetails { get; set; }
    }
}
