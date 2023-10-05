using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace CountryList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public async Task<ActionResult> GetCountry()
        {

            DataTable dt = new DataTable();
            using(var client  = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.com/");
                using (HttpResponseMessage response = await client.GetAsync("/v3.1/all/"))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    dt = (DataTable)JsonConvert.DeserializeObject(responseContent, typeof(DataTable));
                    response.EnsureSuccessStatusCode();

                    return Ok(dt);
                }
            }
           
        }
    }
}

