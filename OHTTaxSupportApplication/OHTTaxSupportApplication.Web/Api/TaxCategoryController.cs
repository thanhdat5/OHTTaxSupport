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
    [RoutePrefix("api/taxcategories")]
    public class TaxCategoryController : ApiControllerBase
    {
        #region Initialize
        private ITaxCategoryService _taxCategoryService;

        public TaxCategoryController(IErrorService errorService, ITaxCategoryService taxCategoryService)
            : base(errorService)
        {
            this._taxCategoryService = taxCategoryService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _taxCategoryService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _taxCategoryService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _taxCategoryService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(TaxCategory obj)
        {
            return _taxCategoryService.Add(obj);
        }

        [Route("update")]
        [HttpPut]
        public ApiResponseViewModel Update(TaxCategory obj)
        {
            return _taxCategoryService.Update(obj);
        }

        [Route("delete")]
        [HttpDelete]
        public ApiResponseViewModel Delete(int id)
        {
            return _taxCategoryService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPut]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _taxCategoryService.SetInActive(id);
        }
    }
}
