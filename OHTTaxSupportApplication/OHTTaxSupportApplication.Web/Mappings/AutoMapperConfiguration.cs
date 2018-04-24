using AutoMapper;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Web.Models;

namespace OHTTaxSupportApplication.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaxValue, TaxValueViewModel>();            
            });
        }

    }
}