using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Accounts = new List<Account>();
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int ID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
