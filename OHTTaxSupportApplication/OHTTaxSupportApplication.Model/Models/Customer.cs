using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Customer
    {
        public int ID { get; set; }
        public int CustomerTypeID { get; set; }
        public string CustomerName { get; set; }
        public int CompanyID { get; set; }
        public string Adderss { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Company Company { get; set; }
        public virtual CustomerType CustomerType { get; set; }
    }
}
