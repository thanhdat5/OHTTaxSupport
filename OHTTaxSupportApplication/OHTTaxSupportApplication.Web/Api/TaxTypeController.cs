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
    [RoutePrefix("api/TaxTypes")]
    public class TaxTypeController : ApiControllerBase
    {
        #region Initialize
        private ITaxTypeService _taxTypeService;

        public TaxTypeController(IErrorService errorService, ITaxTypeService taxTypeService)
            : base(errorService)
        {
            this._taxTypeService = taxTypeService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _taxTypeService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _taxTypeService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _taxTypeService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(TaxType obj)
        {
            return _taxTypeService.Add(obj);
        }

        [Route("update")]
        [HttpPut]
        public ApiResponseViewModel Update(TaxType obj)
        {
            return _taxTypeService.Update(obj);
        }

        [Route("delete")]
        [HttpDelete]
        public ApiResponseViewModel Delete(int id)
        {
            return _taxTypeService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPut]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _taxTypeService.SetInActive(id);
        }
    }
}
