using System;
using System.Collections.Generic;
using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;

namespace Jo2let.Service
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public PropertyService(IPropertyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Property> GetAllPropertys()
        {
            return _repository.GetAll();
        }
        public Property AddProperty(Property property)
        {
            _repository.Add(property);
            _unitOfWork.SaveChanges();
            return property;
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public Property GetPropertyById(int id)
        {
            return _repository.GetById(id);
        }

        public Property UpdateProperty(Property property)
        {
            try
            {
                _repository.Update(property);
                SaveChanges();
                return property;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteProperty(int id)
        {
            var property = _repository.GetById(id);
            _repository.Delete(property);
            SaveChanges();
        }

    }
}
