using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DharaniScaper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DharaniController : ControllerBase
    {
        private  readonly HttpClient client;
        public DharaniScaper.Models.DharaniXContext _db;
        public Services.DharaniService dharaniService;

        public DharaniController()
        {
            _db = new Models.DharaniXContext();
            client = new HttpClient();
            dharaniService = new Services.DharaniService(_db);
        }
        // GET: api/<DharaniController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //await dharaniService.GenerateMandals();
            // await dharaniService.GenerateVillages();
            // await dharaniService.GenerateSurveyNumbers();
            //await dharaniService.GenerateSurveyNumbersForLemoor();
            //await dharaniService.GenerateKhataNumbersForLemoor();
            // await dharaniService.GenerateSurveyInfoForLemoor();
            dharaniService.GenerateResults();
            return new string[] { "value1", "value2" };
        }

        // GET api/<DharaniController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getMandalFromDivisionCitizenPortal?district=17&type=12");
            return "value";
        }

        // POST api/<DharaniController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DharaniController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DharaniController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
