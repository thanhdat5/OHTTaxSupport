using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Unit
    {
        public Unit()
        {                                    
            this.TaxDetails = new List<TaxDetail>();
        }

        public int ID { get; set; }
        public string Value { get; set; }
        public Nullable<bool> IsActive { get; set; }             
        public virtual ICollection<TaxDetail> TaxDetails { get; set; }
    }
}
