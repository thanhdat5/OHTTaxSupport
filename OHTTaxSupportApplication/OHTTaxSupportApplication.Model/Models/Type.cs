using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Type
    {
        public Type()
        {
            this.Invoices = new List<Invoice>();
        }

        public int ID { get; set; }
        public string TypeName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
