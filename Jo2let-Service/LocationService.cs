using System.Collections.Generic;
using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;

namespace Jo2let.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(ILocationRepository repository, IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Location> GetLocations()
        {
            return _repository.GetAll();
        }

        public Location AddLocation(Location location)
        {
            _repository.Add(location);
            SaveChanges();
            return location;
        }

        public Location GetLocationById(int id)
        {
            return _repository.GetById(id);
        }

        public Location UpdateLoaction(Location location)
        {
            _repository.Update(location);
            SaveChanges();
            return location;
        }

        public void DeleteLocation(int id)
        {
            var location = _repository.GetById(id);
            var resources = _propertyRepository.GetMany(r => r.LocationId == id);

            foreach (var item in resources)
            {
                _propertyRepository.Delete(item);
            }
            _repository.Delete(location);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
