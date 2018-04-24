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
    [RoutePrefix("api/Taxs")]
    public class TaxController : ApiControllerBase
    {
        #region Initialize
        private ITaxService _taxService;

        public TaxController(IErrorService errorService, ITaxService taxService)
            : base(errorService)
        {
            this._taxService = taxService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _taxService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _taxService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _taxService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Tax obj)
        {
            return _taxService.Add(obj);
        }

        [Route("update")]
        [HttpPut]
        public ApiResponseViewModel Update(Tax obj)
        {
            return _taxService.Update(obj);
        }

        [Route("delete")]
        [HttpDelete]
        public ApiResponseViewModel Delete(int id)
        {
            return _taxService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPut]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _taxService.SetInActive(id);
        }
    }
}
