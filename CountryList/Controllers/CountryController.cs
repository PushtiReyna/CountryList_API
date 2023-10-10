using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using CountryList.Models;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static CountryList.Models.CountryData;
using System.Text.Json;

namespace CountryList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public async Task<ActionResult> GetCountry()
        {
            #region
            //Get API
            //string strurltest = string.Format("https://restcountries.com/v3.1/all");

            //WebRequest requestObjGet = WebRequest.Create(strurltest);
            //requestObjGet.Method = "GET";

            //HttpWebResponse responseObjGet = null;
            //responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            //string strresulttest = null;
            //using(Stream stream = responseObjGet.GetResponseStream())
            //{
            //    StreamReader sr = new StreamReader(stream);
            //    strresulttest = sr.ReadToEnd();
            //    sr.Close();
            //    return Ok(strresulttest);
            //}
            #endregion

            #region
            //Get api
            //DataTable dt = new DataTable();
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://restcountries.com/");
            //    using (HttpResponseMessage response = await client.GetAsync("/v3.1/all/"))
            //    {
            //        var responseContent = response.Content.ReadAsStringAsync().Result;
            //        dt = (DataTable)JsonConvert.DeserializeObject(responseContent, typeof(DataTable));
            //        response.EnsureSuccessStatusCode();

            //        return Ok(dt);
            //    }
            //}
            #endregion

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("http://192.168.1.164:784/api/GetCountry");

                client.DefaultRequestHeaders.Clear();
                CountryReqModel countrymodel = new CountryReqModel()
                {
                    PageSize = 0,
                    PageNumber = 0,
                    Orderby = true
                };

                var country = JsonConvert.SerializeObject(countrymodel);
                client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                HttpContent requestContent = new StringContent(country, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress, requestContent).Result;

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                CountryGetModel dataTable = new CountryGetModel();

                dataTable = (CountryGetModel)JsonConvert.DeserializeObject(content, (typeof(CountryGetModel)));

                return Ok(dataTable.data);
            }
        }
    }
}









// Country[] cshrpObj = System.Text.Json.JsonSerializer.Deserialize<Country[]>(jsonResults);

// return Ok(cshrpObj.First().name.common);

#region
// Console.WriteLine(cshrpObj[].name);
//  var serializer = new JavaScriptSerializer();

//  List<Country> countryList = (List<Country>)System.Text.Json.JsonSerializer.Deserialize(jsonResults, typeof(List<Country>));
//Country country = new Country();

//var countryList2 = new List<Country>();
//var common = new List<Country>();
//foreach (Country obj in countryList)
//{

//     common = obj.name.common;

//    countryList2.Add(obj.name.common);

//}
//return Ok(countryList2);


//List<Country> countryList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(jsonResults);
// IEnumerable<SelectListItem> countries = (IEnumerable<SelectListItem>)countryList.Select(x => x.name.common);
#region
//Rootobject root = JsonConvert.DeserializeObject<Rootobject>(strresulttest);

//foreach (Class1 obj in root.Property1)
//{
//    string common = obj.name.common;
//}
// sr.Close();
//}

#endregion

// return Ok(countries);
#endregion

