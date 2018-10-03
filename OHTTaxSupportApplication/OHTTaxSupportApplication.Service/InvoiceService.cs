using System;
using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Common;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Data.Repositories;
using OHTTaxSupportApplication.Model.Models;
using OHTTaxSupportApplication.Model.ViewModels;

namespace OHTTaxSupportApplication.Service
{
	public interface IInvoiceService
	{

		ApiResponseViewModel GetAll();
		ApiResponseViewModel Filter(string fromDate, string toDate);

		ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

		ApiResponseViewModel GetById(int id);

		ApiResponseViewModel Add(Invoice invoice);
		ApiResponseViewModel SaveAll(List<InvoiceInput> invoice);

		ApiResponseViewModel Update(Invoice invoice);

		ApiResponseViewModel Delete(int id);

		ApiResponseViewModel SetInActive(int id);

	}

	public class InvoiceService : IInvoiceService
	{
		private IInvoiceRepository _InvoiceRepository;
		private IInvoiceDetailRepository _InvoiceDetailRepository;
		private ICustomerRepository _CustomerRepository;
		private ICategoryRepository _CategoryRepository;

		private IUnitOfWork _unitOfWork;

		public InvoiceService(IInvoiceRepository InvoiceRepository, IInvoiceDetailRepository InvoiceDetailRepository, ICustomerRepository CustomerRepository, ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
		{
			this._InvoiceRepository = InvoiceRepository;
			this._InvoiceDetailRepository = InvoiceDetailRepository;
			this._CustomerRepository = CustomerRepository;
			this._CategoryRepository = CategoryRepository;
			this._unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Get all Invoice
		/// </summary>
		/// <returns></returns>
		public ApiResponseViewModel GetAll()
		{
			var result = new List<InvoiceDetailViewModel>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var invoiceDetails = _InvoiceDetailRepository.GetMulti(m => m.IsActive == true);
				foreach (var idetail in invoiceDetails)
				{
					var invoice = _InvoiceRepository.GetMulti(m => m.IsActive == true && m.ID == idetail.InvoiceID).FirstOrDefault();
					if (invoice != null)
					{
						var obj = new InvoiceDetailViewModel
						{
							ID = invoice.ID,
							InvoiceCode = invoice.InvoiceCode,
							CreateDate = invoice.CreatedDate.ToShortDateString(),
							CustomerID = int.Parse(invoice.CustomerID.ToString()),
							Customer = _CustomerRepository.GetSingleById(int.Parse(invoice.CustomerID.ToString())).CustomerName,
							Category = idetail.Category.CategoryName,
							InOut = invoice.InOut ?? false,
							Status = invoice.Status,
							Value = decimal.Parse(idetail.Value.ToString()).ToString("###,##"),
							IsActive = idetail.IsActive ?? false,
							TaxValue = idetail.TaxValue.Value.ToString()
							//InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(idetail.ID)
						};
						result.Add(obj);
					}

				}
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get all Invoice
		/// </summary>
		/// <returns></returns>
		public ApiResponseViewModel Filter(string fromDate, string toDate)
		{
			var result = new List<InvoiceDetailViewModel>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var invoiceDetails = _InvoiceDetailRepository.GetMulti(m => m.IsActive == true);
				var fromDateValue = !string.IsNullOrEmpty(fromDate) ? DateTime.Parse(fromDate) : DateTime.Now;
				var toDateValue = !string.IsNullOrEmpty(toDate) ? DateTime.Parse(toDate) : DateTime.Now;
				foreach (var idetail in invoiceDetails)
				{
					var invoice = _InvoiceRepository.GetMulti(
							m => m.IsActive == true
							&& m.ID == idetail.InvoiceID
							 && (m.CreatedDate >= fromDateValue)
							&& (m.CreatedDate <= toDateValue)
						  //&& (string.IsNullOrEmpty(fromDate.Trim()) || m.CreatedDate >= fromDateValue)
						  //&& (string.IsNullOrEmpty(toDate.Trim()) || m.CreatedDate <= toDateValue)
						  ).FirstOrDefault();
					if (invoice != null)
					{
						var obj = new InvoiceDetailViewModel
						{
							ID = invoice.ID,
							InvoiceCode = invoice.InvoiceCode,
							CreateDate = invoice.CreatedDate.ToShortDateString(),
							CustomerID = int.Parse(invoice.CustomerID.ToString()),
							Customer = _CustomerRepository.GetSingleById(int.Parse(invoice.CustomerID.ToString())).CustomerName,
							Category = idetail.Category.CategoryName,
							InOut = invoice.InOut ?? false,
							Status = invoice.Status,
							Value = decimal.Parse(idetail.Value.ToString()).ToString("###,##"),
							IsActive = idetail.IsActive ?? false,
							TaxValue = idetail.TaxValue.Value.ToString()
							//InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(idetail.ID)
						};
						result.Add(obj);
					}

				}
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get all invoice with pagging
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="pageSize">Page size</param>
		/// <returns></returns>
		public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
		{
			var result = new List<InvoiceViewModel>();
			var paginationSet = new PaginationSet<InvoiceViewModel>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var currentPage = page ?? 1;
				int totalRow = 0;
				var query = _InvoiceRepository.GetMulti(m => m.IsActive == true);
				totalRow = query.Count();

				result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
					.Take(pageSize)
					.Select(m => new InvoiceViewModel
					{
						ID = m.ID,
						InvoiceCode = m.InvoiceCode,
						CreatedDate = m.CreatedDate != null ? m.CreatedDate : DateTime.Now,
						CustomerID = m.CustomerID,
						InOut = m.InOut,
						Status = m.Status,
						IsActive = m.IsActive ?? false,
						Value = m.Value,
						InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(m.ID)
					})
					.ToList();

				paginationSet = new PaginationSet<InvoiceViewModel>()
				{
					Items = result,
					Page = currentPage,
					TotalCount = result.Count(),
					TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
				};
				response.Result = paginationSet;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get invoice by id
		/// </summary>
		/// <param name="id">ID of invoice</param>
		/// <returns></returns>
		public ApiResponseViewModel GetById(int id)
		{
			var result = new InvoiceViewModel();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
				if (exists)
				{
					var tempResult = _InvoiceRepository.GetSingleById(id);
					result.ID = tempResult.ID;
					result.InvoiceCode = tempResult.InvoiceCode;
					result.CreatedDate = tempResult.CreatedDate;
					result.CustomerID = tempResult.CustomerID;
					result.InOut = tempResult.InOut;
					result.Status = tempResult.Status;
					result.IsActive = tempResult.IsActive ?? false;
					result.Value = tempResult.Value;
					result.InvoiceDetails = _InvoiceDetailRepository.GetListInvoiceDetailsByInvoiceID(result.ID);
					response.Result = result;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Add new invoice
		/// </summary>
		/// <param name="obj">invoice</param>
		/// <returns></returns>
		public ApiResponseViewModel Add(Invoice obj)
		{
			var result = new Invoice();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var currentDate = DateTime.Now;
				obj.InvoiceCode = "HD" + currentDate.Year + "" + currentDate.Month + "" + currentDate.Day + "" + currentDate.Hour + "" + currentDate.Minute;
				obj.CreatedDate = DateTime.Now;
				result = _InvoiceRepository.Add(obj);
				_unitOfWork.Commit();
				response.Message = CommonConstants.AddSuccess;
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;

		}

		/// <summary>
		/// Save all invoices
		/// </summary>
		/// <param name="lst">invoices</param>
		/// <returns></returns>
		public ApiResponseViewModel SaveAll(List<InvoiceInput> lst)
		{
			var result = new List<InvoiceDetail>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				foreach (var item in lst)
				{
					decimal totalValue = 0;
					var customerID = 0;
					var inOut = false;
					var invoiceCode = "";
					foreach (var sub in item.details)
					{
						var obj = new InvoiceDetail();
						obj.ID = 0;
						obj.InvoiceID = item.id;
						obj.Value = sub.Value;
						obj.DepartmentID = sub.DepartmentID;
						obj.CategoryID = sub.CategoryID;
						obj.TaxValueID = sub.TaxValue;
						obj.IsActive = true;
						obj.TaxValueID = sub.TaxValue;
						result.Add(_InvoiceDetailRepository.Add(obj));

						totalValue += sub.Value;
						customerID = sub.CustomerID;
						inOut = sub.InOut;
						invoiceCode = sub.InvoiceCode;
					}
					var invoice = _InvoiceRepository.GetSingleById(item.id);
					invoice.InvoiceCode = invoiceCode;
					invoice.InOut = inOut;
					invoice.Value = totalValue;
					invoice.CustomerID = customerID;
					invoice.IsActive = true;
					_InvoiceRepository.Update(invoice);
				}
				_unitOfWork.Commit();
				response.Message = CommonConstants.AddSuccess;
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;

		}

		/// <summary>
		/// Update invoice
		/// </summary>
		/// <param name="obj">New invoice</param>
		/// <returns></returns>
		public ApiResponseViewModel Update(Invoice obj)
		{
			var result = new InvoiceViewModel();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _InvoiceRepository.CheckContains(m => m.ID == obj.ID);
				if (exists)
				{
					_InvoiceRepository.Update(obj);
					_unitOfWork.Commit();
					response.Message = CommonConstants.SaveSuccess;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}

			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Delete invoice by Id
		/// </summary>
		/// <param name="id">ID of invoice</param>
		/// <returns></returns>
		public ApiResponseViewModel Delete(int id)
		{
			var result = new Invoice();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
				if (exists)
				{
					result = _InvoiceRepository.Delete(id);
					_unitOfWork.Commit();
					response.Message = CommonConstants.DeleteSuccess;
					response.Result = result;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}

			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Make invoice in-active by Id
		/// </summary>
		/// <param name="id">ID of invoice</param>
		/// <returns></returns>
		public ApiResponseViewModel SetInActive(int id)
		{
			var result = new Invoice();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _InvoiceRepository.CheckContains(m => m.ID == id);
				if (exists)
				{
					result = _InvoiceRepository.GetSingleById(id);
					result.IsActive = false;
					_InvoiceRepository.Update(result);
					_unitOfWork.Commit();
					response.Message = CommonConstants.DeleteSuccess;
					var temp = new InvoiceViewModel();
					temp.ID = result.ID;
					response.Result = temp;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}
	}
}