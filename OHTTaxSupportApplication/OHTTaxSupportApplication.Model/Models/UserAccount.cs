using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class UserAccount
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Account Account { get; set; }
        public virtual User User { get; set; }
    }
}
