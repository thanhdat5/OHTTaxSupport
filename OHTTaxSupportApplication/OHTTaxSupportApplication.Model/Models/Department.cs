using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Taxes = new List<Tax>();
        }

        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public int CompanyID { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
