using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class InvoiceDetail
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public int DepartmentID { get; set; }
        public int CategoryID { get; set; }
        public int TaxValueID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual TaxValue TaxValue { get; set; }
        public virtual Category Category { get; set; }
        public virtual Department Department { get; set; }
    }
}
