using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int ID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<bool> InOut { get; set; }
        public int Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Type Type { get; set; }
        public virtual TaxValue TaxValue { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
