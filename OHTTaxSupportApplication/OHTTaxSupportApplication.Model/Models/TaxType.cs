using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class TaxType
    {
        public TaxType()
        {
            this.Taxes = new List<Tax>();
        }

        public int ID { get; set; }
        public string TaxTypeName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
