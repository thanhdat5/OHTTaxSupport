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
    [RoutePrefix("api/departments")]
    public class DepartmentController : ApiControllerBase
    {
        #region Initialize
        private IDepartmentService _departmentService;

        public DepartmentController(IErrorService errorService, IDepartmentService departmentService)
            : base(errorService)
        {
            this._departmentService = departmentService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _departmentService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _departmentService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _departmentService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Department obj)
        {
            return _departmentService.Add(obj);
        }

        [Route("update")]
        [HttpPut]
        public ApiResponseViewModel Update(Department obj)
        {
            return _departmentService.Update(obj);
        }

        [Route("delete")]
        [HttpDelete]
        public ApiResponseViewModel Delete(int id)
        {
            return _departmentService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPut]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _departmentService.SetInActive(id);
        }
    }
}
