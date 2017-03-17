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
            AutoMapper.Mapper.Map(propertyList, propertyViewModels);
            return Ok(propertyViewModels);

        }


        public IHttpActionResult Get(int id)
        {
            var property = _propertyService.GetPropertyById(id);
            var propertyViewModel = new PropertyViewModel();
            AutoMapper.Mapper.Map(property, propertyViewModel);
            return Ok(propertyViewModel);
        }

        public IHttpActionResult Post(CreatePropertyViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = new Property();
            AutoMapper.Mapper.Map(createModel, propertyData);
            _propertyService.AddProperty(propertyData);


            var propertyViewModel = new PropertyViewModel();
            AutoMapper.Mapper.Map(propertyData, propertyViewModel);

            return Created(
                new Uri(Request.RequestUri + "api/properties" + propertyViewModel.Id),
                propertyViewModel);
        }



        public IHttpActionResult Put(EditPropertyViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = _propertyService.GetPropertyById(editModel.Id);
            if (propertyData == null)
                return NotFound();

            AutoMapper.Mapper.Map(editModel, propertyData);
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
