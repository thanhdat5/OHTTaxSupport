using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class InvoiceDetail
    {
        public int ID { get; set; }
        public bool InOut { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int UnitID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public double Quanlity { get; set; }
        public int DepartmentID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Department Department { get; set; }
        public virtual Category Category { get; set; }
    }
}
