using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Jo2let.Api.Models.Location;
using Jo2let.Model;
using Jo2let.Service;

namespace Jo2let.Api.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IHttpActionResult Get()
        {
            var locationList = _locationService.GetLocations();
            var locationViewModels = new List<LocationViewModel>();
            AutoMapper.Mapper.Map(locationList, locationViewModels);
            return Ok(locationViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            var location = _locationService.GetLocationById(id);
            var locationViewModel = new LocationViewModel();
            AutoMapper.Mapper.Map(location, locationViewModel);
            return Ok(locationViewModel);
        }

        public IHttpActionResult Post(CreateLocationViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationData = new Location();
            AutoMapper.Mapper.Map(createModel, locationData);
            _locationService.AddLocation(locationData);

            var locationViewModel = new LocationViewModel();
            AutoMapper.Mapper.Map(locationData, locationViewModel);

            return Created(new Uri(Request.RequestUri + "api/loctions" + locationViewModel.Id), locationViewModel);

        }

        public IHttpActionResult Put(EditLocationViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationData = _locationService.GetLocationById(editModel.Id);
            if (locationData == null)
                return NotFound();

            AutoMapper.Mapper.Map(editModel, locationData);
            _locationService.UpdateLoaction(locationData);

            return StatusCode(HttpStatusCode.NoContent);

        }

        public IHttpActionResult Delete(int id)
        {
            var locationData = _locationService.GetLocationById(id);
            if (locationData == null)
                return NotFound();

            _locationService.DeleteLocation(id);

            return StatusCode(HttpStatusCode.NoContent);

        }

    }
}
