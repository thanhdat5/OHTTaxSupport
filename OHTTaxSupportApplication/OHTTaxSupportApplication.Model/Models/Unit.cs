using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Unit
    {
        public Unit()
        {                                    
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int ID { get; set; }
        public string Value { get; set; }
        public Nullable<bool> IsActive { get; set; }             
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
