﻿using System;
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
    [RoutePrefix("api/CustomerTypes")]
    public class CustomerTypeController : ApiControllerBase
    {
        #region Initialize
        private ICustomerTypeService _customerTypeService;

        public CustomerTypeController(IErrorService errorService, ICustomerTypeService customerTypeService)
            : base(errorService)
        {
            this._customerTypeService = customerTypeService;
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
            return _customerTypeService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(CustomerType obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(CustomerType obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _customerTypeService.SetInActive(id);
        }
    }
}
