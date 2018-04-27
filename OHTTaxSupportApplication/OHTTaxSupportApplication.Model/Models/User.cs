using System;
using System.Collections.Generic;

namespace OHTTaxSupportApplication.Model.Models
{
    public partial class User
    {
        public User()
        {
            this.UserAccounts = new List<UserAccount>();
        }

        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int CompanyID { get; set; }              
        public string Image { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
        public DateTime LastOnline { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
