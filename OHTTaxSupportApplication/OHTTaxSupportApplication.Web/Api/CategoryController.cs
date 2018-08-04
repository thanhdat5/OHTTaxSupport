using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using OHTTaxSupportApplication.Common;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;
using OHTTaxSupportApplication.Service;
using OHTTaxSupportApplication.Web.Infrastructure.Core;

namespace OHTTaxSupportApplication.Web.Api
{
    [RoutePrefix("api/Categories")]
    public class CategoryController : ApiControllerBase
    {
        #region Initialize
        private ICategoryService _CategoryService;

        public CategoryController(IErrorService errorService, ICategoryService CategoryService)
            : base(errorService)
        {
            this._CategoryService = CategoryService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Category obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(Category obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _CategoryService.SetInActive(id);
        }
    }
}
