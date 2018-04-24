using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            this.Customers = new List<Customer>();
        }

        public int ID { get; set; }
        public string CustomerTypeName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
