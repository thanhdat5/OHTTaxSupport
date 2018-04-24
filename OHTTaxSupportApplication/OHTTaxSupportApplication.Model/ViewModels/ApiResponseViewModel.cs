using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTTaxSupportApplication.Model.ViewModels
{
    public class ApiResponseViewModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
