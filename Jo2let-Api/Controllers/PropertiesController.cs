using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Jo2let.Api.Models.Location;
using Jo2let.Api.Models.Property;
using Jo2let.Model;
using Jo2let.Service;

namespace Jo2let.Api.Controllers
{
    public class PropertiesController : ApiController
    {
        private readonly IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public IHttpActionResult Get()
        {
            var propertyList = _propertyService.GetAllPropertys();
            var propertyViewModels = new List<PropertyViewModel>();
            foreach (var property in propertyList)
            {
                propertyViewModels.Add(new PropertyViewModel
                {
                    Id = property.Id,
                    Title = property.Title,
                    Description = property.Description,
                    Location = new LocationViewModel
                    {
                        Id = property.Location.Id,
                        Name = property.Location.Name
                    }
                });
            }
            return Ok(propertyViewModels);

        }


        public IHttpActionResult Get(int id)
        {
            var property = _propertyService.GetPropertyById(id);
            var propertyViewModel = new PropertyViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Location = new LocationViewModel
                {
                    Id = property.Location.Id,
                    Name = property.Location.Name
                }
            };

            return Ok(propertyViewModel);
        }

        public IHttpActionResult Post(CreatePropertyViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = new Property
            {
                Title = createModel.Title,
                Description = createModel.Description,
                LocationId = createModel.LocationId
            };

            _propertyService.AddProperty(propertyData);

            var property = new PropertyViewModel
            {
                Id = propertyData.Id,
                Title = propertyData.Title,
                Description = propertyData.Description,
                Location = new LocationViewModel
                {
                    Id = propertyData.Location.Id,
                    Name = propertyData.Location.Name
                }

            };
            return Created(
                new Uri(Request.RequestUri + "api/properties" + property.Id),
                property);
        }



        public IHttpActionResult Put(EditPropertyViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = _propertyService.GetPropertyById(editModel.Id);
            if (propertyData == null)
                return NotFound();

            propertyData.Id = editModel.Id;
            propertyData.Title = editModel.Title;
            propertyData.Description = editModel.Description;
            propertyData.LocationId = editModel.LocationId;

            _propertyService.UpdateProperty(propertyData);

            return StatusCode(HttpStatusCode.NoContent);

        }

        public IHttpActionResult Delete(int id)
        {

            var propertyData = _propertyService.GetPropertyById(id);
            if (propertyData == null)
                return NotFound();

            _propertyService.DeleteProperty(id);

            return StatusCode(HttpStatusCode.NoContent);

        }

    }
}
