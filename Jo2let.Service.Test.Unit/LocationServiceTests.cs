using System.Collections.Generic;
using System.Linq;
using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Model;
using Moq;
using NUnit.Framework;

namespace Jo2let.Service.Test.Unit
{
    [TestFixture]
    public class LocationServiceTests
    {
        private readonly Mock<ILocationRepository> _repository;
        private readonly LocationService _locationService;

        public LocationServiceTests()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<ILocationRepository>();

            _locationService = new LocationService(_repository.Object, unitOfWork.Object);
        }

        [Test]
        public void WhenGettingLocationById_ThenTheCorrectDataReturned()
        {
            var location = new Location
            {
                Id = 100,
                Name = "Test"
            };
            _repository.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(location);

            var result = _locationService.GetLocationById(100);

            Assert.AreEqual(100, result.Id);
            Assert.AreEqual("Test", result.Name);
        }

        [Test]
        public void WhenGettingAllLocation_ThenTheCorrectListOfDataReturned()
        {
            var locations = new List<Location>
            {
                new Location
                {
                    Id = 100,
                    Name = "Test1"
                },
                new Location
                {
                    Id = 101,
                    Name = "Test2"
                }
            };

            _repository.Setup(repository => repository.GetAll()).Returns(locations);

            var result = _locationService.GetLocations().ToList();
            var firstRecord = result.First();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, firstRecord.Id);
            Assert.AreEqual("Test1", firstRecord.Name);
        }

    }
}
