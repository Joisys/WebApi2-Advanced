using Jo2let.Infrastructure;
using Jo2let.Infrastructure.Repository;
using Jo2let.Interface.Interface;
using Jo2let.Model;
using NUnit.Framework;

namespace Jo2let.Data.Test.Integartion
{
    [TestFixture]
    public class LocationRepositoryTests : EffortTestBase
    {
        private PropertyDbContext _context;
        private LocationRepository _repository;
        private AutoPocoTestDataConfig _autoPocoConfig;
        private IUnitOfWork _unitOfWork;
        private DatabaseFactory _databaseFactory;

        [SetUp]
        public void GivenAPropertyContext()
        {
            _context = PropertyDbTestContext;
            _databaseFactory = new DatabaseFactory();
            _unitOfWork = new UnitOfWork(_databaseFactory, _context);

            _repository = new LocationRepository(_databaseFactory);
            _autoPocoConfig = new AutoPocoTestDataConfig();
        }


        [Test]
        public void WhenGettingLocation_WithNonExistingId_ReturnsNull()
        {
            const int nonExistingId = 100;
            var location = _repository.GetById(nonExistingId);
            Assert.Null(location);
        }

        [Test]
        public void WhenGettingLocation_WithExistingId_ReturnsCorrectData()
        {
            //Arrange
            var locationData = _autoPocoConfig.Session.Single<Location>()
                .Impose(l => l.Name, "LocationTest")
                .Get();

            _context.Locations.Add(locationData);
            _unitOfWork.SaveChanges();

            //Act
            var location = _repository.GetById(locationData.Id);

            //Assert
            Assert.NotNull(location);
            Assert.AreEqual("LocationTest", location.Name);
        }

    }
}
