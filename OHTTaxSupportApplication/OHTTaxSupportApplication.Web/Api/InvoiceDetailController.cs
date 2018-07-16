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
    [RoutePrefix("api/InvoiceDetails")]
    public class InvoiceDetailController : ApiControllerBase
    {
        #region Initialize
        private IInvoiceDetailService _InvoiceDetailService;

        public InvoiceDetailController(IErrorService errorService, IInvoiceDetailService InvoiceDetailService)
            : base(errorService)
        {
            this._InvoiceDetailService = InvoiceDetailService;
        }

        #endregion

        [Route("getall")]
        [HttpGet]
        public ApiResponseViewModel GetAll()
        {
            return _InvoiceDetailService.GetAll();
        }

        [Route("getallwithpaging")]
        [HttpGet]
        public ApiResponseViewModel GetAllWithPagging(int page, int pageSize)
        {
            return _InvoiceDetailService.GetAllWithPagging(page, pageSize);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetById(int id)
        {
            return _InvoiceDetailService.GetById(id);
        }

        [Route("getbyinvoiceid/{id:int}")]
        [HttpGet]
        public ApiResponseViewModel GetByInvoiceId(int id)
        {
            return _InvoiceDetailService.GetByInvoiceId(id);
        }

        [Route("create")]
        [HttpPost]
        public ApiResponseViewModel Create(InvoiceDetail obj)
        {
            return _InvoiceDetailService.Add(obj);
        }

        [Route("update")]
        [HttpPost]
        public ApiResponseViewModel Update(InvoiceDetail obj)
        {
            return _InvoiceDetailService.Update(obj);
        }

        [Route("delete")]
        [HttpPost]
        public ApiResponseViewModel Delete(int id)
        {
            return _InvoiceDetailService.Delete(id);
        }

        [Route("setinactive")]
        [HttpPost]
        public ApiResponseViewModel SetInActive(int id)
        {
            return _InvoiceDetailService.SetInActive(id);
        }
    }
}
