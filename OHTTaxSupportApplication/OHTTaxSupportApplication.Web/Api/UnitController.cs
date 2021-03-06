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
    [RoutePrefix("api/Units")]
    public class UnitController : ApiControllerBase
    {
        #region Initialize
        private IUnitService _unitService;

        public UnitController(IErrorService errorService, IUnitService unitService)
            : base(errorService)
        {
            this._unitService = unitService;
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
            return _unitService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Unit obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(Unit obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _unitService.SetInActive(id);
        }
    }
}
