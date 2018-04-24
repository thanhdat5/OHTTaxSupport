using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Company
    {
        public Company()
        {
            this.Customers = new List<Customer>();
            this.Departments = new List<Department>();
            this.Users = new List<User>();
        }

        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
