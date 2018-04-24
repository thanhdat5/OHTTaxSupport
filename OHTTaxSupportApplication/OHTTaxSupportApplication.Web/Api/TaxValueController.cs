using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;
using OHTTaxSupportApplication.Service;
using OHTTaxSupportApplication.Web.Infrastructure.Core;

namespace OHTTaxSupportApplication.Web.Api
{
    [RoutePrefix("api/taxvalue")]
    public class TaxValueController : ApiControllerBase
    {
        #region Initialize
        private ITaxValueService _taxValueService;

        public TaxValueController(IErrorService errorService, ITaxValueService taxValueService)
            : base(errorService)
        {
            this._taxValueService = taxValueService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _taxValueService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _taxValueService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _taxValueService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(TaxValue obj)
        {
            return _taxValueService.Add(obj);
        }

        [Route("update")]
        [HttpPut]
        public ApiResponseViewModel Update(TaxValue obj)
        {
            return _taxValueService.Update(obj);
        }

        [Route("delete")]
        [HttpDelete]
        public ApiResponseViewModel Delete(int id)
        {
            return _taxValueService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPut]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _taxValueService.SetInActive(id);
        }
    }
}
