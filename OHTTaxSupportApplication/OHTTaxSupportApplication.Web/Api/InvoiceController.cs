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
    [RoutePrefix("api/Invoices")]
    public class InvoiceController : ApiControllerBase
    {
        #region Initialize
        private IInvoiceService _invoiceService;

        public InvoiceController(IErrorService errorService, IInvoiceService invoiceService)
            : base(errorService)
        {
            this._invoiceService = invoiceService;
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
            return _invoiceService.GetAll();
        }

        [Route("filter")]
        [HttpGet]
        public ApiResponseViewModel Filter(string fromDate, string toDate)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.Filter(fromDate, toDate);
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.GetById(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(Invoice obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.Add(obj);
        }

        [Route("saveAll")]
        [HttpPost]
        public ApiResponseViewModel SaveAll(List<InvoiceInput> lst)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.SaveAll(lst);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(Invoice obj)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            if (HttpContext.Current.Session["UserLogged"] == null)
            {
                return CommonConstants.accessDenied;
            }
            return _invoiceService.SetInActive(id);
        }
    }
}
