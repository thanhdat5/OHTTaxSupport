using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class PaginationSet<T> where T : class
    {
        public object Items { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
