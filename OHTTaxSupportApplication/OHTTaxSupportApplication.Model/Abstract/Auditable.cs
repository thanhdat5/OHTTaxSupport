using System;
using System.ComponentModel.DataAnnotations;

namespace OHTTaxSupportApplication.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public int ID { set; get; }
        public bool IsActive { get; set; }  
    }
}