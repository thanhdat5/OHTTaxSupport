using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Product
    {
        public Product()
        {
            this.InvoiceDetails = new List<InvoiceDetail>();
        }

        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int UnitID { get; set; }
        public Nullable<int> UnitID2 { get; set; }
        public Nullable<bool> IsActive { get; set; }    
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
