using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class TaxDetail
    {
        public int ID { get; set; }
        public int TaxID { get; set; }
        public int ProductID { get; set; }
        public int UnitID { get; set; }
        public Nullable<decimal> Value { get; set; }
        public double Quanlity { get; set; }
        public string Note { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
