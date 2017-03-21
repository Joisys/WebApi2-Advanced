using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Jo2let.Api.Controllers;
using Jo2let.Api.Mapper;
using Jo2let.Api.Models.Location;
using Jo2let.Model;
using Jo2let.Service;
using Moq;
using NUnit.Framework;

namespace Jo2let.Api.Test.Acceptance
{

    public class LocationControllerTests
    {
        private readonly Mock<ILocationService> _service;
        private readonly Mock<IMapper> _mockMapper;

        public LocationControllerTests()
        {
            _service = new Mock<ILocationService>();
            _mockMapper = new Mock<IMapper>();
            AutoMapperConfiguration.Configure();
        }

        [Test]
        public void WhenRequestingAGivenLocationById_ReturnesCorrectResponseData()
        {
            // Arrange
            _service.Setup(locationService => locationService.GetLocationById(10)).Returns(new Location
            {
                Id = 10,
                Name = "Location1"
            });

            _mockMapper.Setup(x => x.Map<Location, LocationViewModel>(It.IsAny<Location>()))
                .Returns(new LocationViewModel
                {
                    Id = 10,
                    Name = "Location1"
                });

            var controller = new LocationsController(_service.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var response = controller.Get(10);
            var contentResult = response as OkNegotiatedContentResult<LocationViewModel>;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(10, contentResult.Content.Id);
            Assert.AreEqual("Location1", contentResult.Content.Name);
        }
    }
}
