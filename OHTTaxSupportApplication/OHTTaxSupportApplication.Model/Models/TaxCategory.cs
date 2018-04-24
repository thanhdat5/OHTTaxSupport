using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class TaxCategory
    {
        public TaxCategory()
        {
            this.Accounts = new List<Account>();
            this.Taxes = new List<Tax>();
        }

        public int ID { get; set; }
        public string Category { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
