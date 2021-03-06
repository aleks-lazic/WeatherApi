﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WeatherAPI;
using WeatherAPI.Controllers;

namespace WeatherApi.Controllers
{
    public class WeatherController : ApiController
    {

        private Model1Container context = new Model1Container();

        [HttpGet]
        [Route("api/weather/{cityName}")]
        public IEnumerable<Weather> GetWeatherByCity(string cityName)
        {
            if (!GlobalController.flag)
            {
                foreach (var item in context.Weather)
                {
                    context.Weather.Remove(item);
                }

                context.SaveChanges();

                for (int i = 0; i < 3; i++)
                {
                    CreateWeather("Crans-Montana", DateTime.Now.AddDays(i));
                    CreateWeather("Sierre", DateTime.Now.AddDays(i));
                    CreateWeather("Sion", DateTime.Now.AddDays(i));
                    CreateWeather("Lausanne", DateTime.Now.AddDays(i));

                }
            }

            GlobalController.flag = true;

            var q = from w in context.Weather.Include("City").Where(s => s.City.name.ToLower().Contains(cityName))
                    .OrderBy(s => s.date)
                    select w;
            return q.ToList();
        }

        [HttpPost]
        [Route("api/weather/")]
        public IHttpActionResult PostWeather([FromBody]Weather w)
        {
            context.City.Attach(w.City);
            //first check if a weather already exists at the date we are creating
            foreach (var item in context.Weather)
            {
                if (item.City.name.Equals(w.City.name))
                {
                    if (item.date.Date.Equals(w.date.Date))
                    {
                        context.Weather.Remove(item);
                    }
                }
            }
            context.SaveChanges();

            //now save the new weather in the DB

            context.Weather.Add(w);
            context.SaveChanges();
            return Ok(w);
        }

        [HttpPut]
        [Route("api/weather/")]
        public IHttpActionResult PutWeather([FromBody]Weather w)
        {
            Weather weather = context.Weather.Where(we => we.Id == w.Id).First();
            context.City.Attach(w.City);
            weather.degreeMorning = w.degreeMorning;
            weather.degreeAfternoon = w.degreeAfternoon;
            weather.precipitation = w.precipitation;
            weather.humidity = w.humidity;
            weather.wind = w.wind;
            weather.date = w.date;
            weather.City = w.City;
            context.SaveChanges();
            return Ok(weather);
        }

        [HttpDelete]
        [Route("api/weather/{id}")]
        public IHttpActionResult DeleteWeather(int id)
        {
            var w = context.Weather.Include("City").Where(we => we.Id == id).First();
            context.Weather.Remove(w);
            context.SaveChanges();
            return Ok(w);
        }

        private void CreateWeather(string cityName, DateTime date)
        {
            Random rnd = new Random();
            int degreeMorning = rnd.Next(1, 25);
            int degreeAfternoon = degreeMorning + 5;
            int precipitation = rnd.Next(1, 100);
            int humidity = rnd.Next(1, 40);
            int wind = rnd.Next(1, 50);

            Weather weather = new Weather { degreeMorning = degreeMorning, degreeAfternoon = degreeAfternoon, precipitation = precipitation, humidity = humidity, wind = wind, date = date };
            weather.City = context.City.Where(s => s.name.Equals(cityName)).First();
            context.Weather.Add(weather);
            context.SaveChanges();
        }
    }
}