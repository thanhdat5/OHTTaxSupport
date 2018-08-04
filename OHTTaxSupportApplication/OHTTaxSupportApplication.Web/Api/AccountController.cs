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
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiControllerBase
    {
        #region Initialize
        private IAccountService _accountService;

        public AccountController(IErrorService errorService, IAccountService accountService)
            : base(errorService)
        {
            this._accountService = accountService;
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
            return _accountService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Account obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(Account obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _accountService.SetInActive(id);
        }
    }
}
