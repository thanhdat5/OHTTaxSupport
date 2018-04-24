using System.Collections.Generic;
using System.Linq;
using OHTTaxSupportApplication.Common;
using OHTTaxSupportApplication.Data.Infrastructure;
using OHTTaxSupportApplication.Data.Repositories;
using OHTTaxSupportApplication.Model.Models;

namespace OHTTaxSupportApplication.Service
{
    public interface ITaxValueService
    {
        TaxValue Add(TaxValue taxValue);

        void Update(TaxValue taxValue);

        TaxValue Delete(int id);

        IEnumerable<TaxValue> GetAll();             

        TaxValue GetById(int id);

        void Save();       

    }

    public class TaxValueService : ITaxValueService
    {
        private ITaxValueRepository _taxValueRepository;    

        private IUnitOfWork _unitOfWork;

        public TaxValueService(ITaxValueRepository taxValueRepository, IUnitOfWork unitOfWork)
        {
            this._taxValueRepository = taxValueRepository; 
            this._unitOfWork = unitOfWork;
        }

        public TaxValue Add(TaxValue obj)
        {
            var taxValue = _taxValueRepository.Add(obj);
            _unitOfWork.Commit();
            return taxValue;
        }

        public TaxValue Delete(int id)
        {
            return _taxValueRepository.Delete(id);
        }

        public IEnumerable<TaxValue> GetAll()
        {
            return _taxValueRepository.GetAll();
        }  

        public TaxValue GetById(int id)
        {
            return _taxValueRepository.GetSingleById(id);
        } 

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(TaxValue obj)
        {
            _taxValueRepository.Update(obj); 
        }   
    }
}