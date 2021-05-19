using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DharaniScaper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public DharaniScaper.Models.DharaniXContext _db;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _db = new Models.DharaniXContext();
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Test()
        //{

        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        //        https://dharani.telangana.gov.in/getMandalFromDivisionCitizenPortal?district=17&type=12


        //https://dharani.telangana.gov.in/getVillageFromMandalCitizenPortal?mandalId=335&type=13


        //https://dharani.telangana.gov.in/getSurveyCitizen?villId=1701006&flag=survey

        //https://dharani.telangana.gov.in/getKhataNoCitizen?villId=1701006&flag=khatanos&surveyNo=1/B/4

        //https://dharani.telangana.gov.in/getPublicDataInfo?villageId=610022&flagToSearch=surveynumber&searchData=5/B&flagval=district&district=6&mandal=114&divi=&khataNoIdselect=60053

        ///getPublicDataInfo? villageId = 2206013 & flagToSearch = surveynumber & searchData = 1 / 1 / 2 & flagval = district & district = 22 & mandal = 417 & divi = &khataNoIdselect = 9008





        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var x = _db.Districts.ToList();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
