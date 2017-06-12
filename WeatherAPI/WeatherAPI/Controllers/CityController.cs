using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeatherAPI.Controllers
{
    public class CityController : ApiController
    {

        private Model1Container context = new Model1Container();

        [HttpGet]
        [Route("api/city")]
        public IHttpActionResult GetAllCities()
        {
            var cities = from c in context.City
                         .OrderBy(ci => ci.name)
                         select c;

            return Ok(cities.ToList());
        }

        [HttpGet]
        [Route("api/city/{cityName}")]
        public IHttpActionResult GetCityByName(string cityName)
        {
            var city = from v in context.City.Where(c => c.name.Contains(cityName))
                       select v;

            return Ok(city.First());

        }
    }
}
