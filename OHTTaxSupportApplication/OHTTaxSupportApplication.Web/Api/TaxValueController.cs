using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Service;
using OHTTaxSupportApplication.Web.Infrastructure.Core;
using OHTTaxSupportApplication.Web.Models;

namespace OHTTaxSupportApplication.Web.Api
{
    [RoutePrefix("api/taxvalue")]
    public class TaxValueController : ApiControllerBase
    {
        #region Initialize
        private ITaxValueService _taxValueService;

        public TaxValueController(IErrorService errorService, ITaxValueService taxValueService)
            : base(errorService)
        {
            this._taxValueService = taxValueService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _taxValueService.GetAll();

                var responseData = Mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _taxValueService.GetById(id);

                var responseData = Mapper.Map<TaxValue, TaxValueViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _taxValueService.GetAll();

                totalRow = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<TaxValue>, IEnumerable<TaxValueViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<TaxValueViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }  
    }
}
