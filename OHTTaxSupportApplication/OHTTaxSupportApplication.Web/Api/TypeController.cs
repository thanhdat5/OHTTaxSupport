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
    [RoutePrefix("api/Types")]
    public class TypeController : ApiControllerBase
    {
        #region Initialize
        private ITypeService _TypeService;

        public TypeController(IErrorService errorService, ITypeService TypeService)
            : base(errorService)
        {
            this._TypeService = TypeService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _TypeService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _TypeService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _TypeService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Model.Models.Type obj)
        {
            return _TypeService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(Model.Models.Type obj)
        {
            return _TypeService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            return _TypeService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _TypeService.SetInActive(id);
        }
    }
}
