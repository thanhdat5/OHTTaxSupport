using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Account
    {
        public Account()
        {
            this.UserAccounts = new List<UserAccount>();
        }

        public int ID { get; set; }
        public string AccountCode { get; set; }
        public int TaxCategoryID { get; set; }
        public int TaxValueID { get; set; }
        public string SH { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual TaxCategory TaxCategory { get; set; }
        public virtual TaxValue TaxValue { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
