using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class TaxValue
    {
        public TaxValue()
        {
            this.Accounts = new List<Account>();
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int ID { get; set; }
        public double Value { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
