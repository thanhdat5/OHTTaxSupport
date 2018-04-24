using System;
using System.ComponentModel.DataAnnotations;

namespace OHTTaxSupportApplication.Web.Models
{
    public class TaxValueViewModel
    {
        public int ID { set; get; }
        public double Value { set; get; }
        public bool IsActive { set; get; }          
    }
}