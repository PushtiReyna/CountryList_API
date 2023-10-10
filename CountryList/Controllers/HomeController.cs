using CountryList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using static CountryList.Models.CountryData;
using System.Text;

namespace CountryList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Countrylist()
        {
            return View();
        }
        //public ActionResult GetCountry()
        //{
        //    string strurltest = string.Format("https://restcountries.com/v3.1/all");
        //    WebRequest requestObjGet = WebRequest.Create(strurltest);
        //    requestObjGet.Method = "GET";
        //    HttpWebResponse responseObjGet = null;
        //    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

        //    string strresulttest = null;

        //    using (Stream stream = responseObjGet.GetResponseStream())
        //    {
        //        StreamReader sr = new StreamReader(stream);
        //        strresulttest = sr.ReadToEnd();

        //        sr.Close();
        //        return View(strresulttest);
        //    }

        //}

        public async Task<IActionResult> GetCountry()
        {
            #region
            // using (var client = new HttpClient())
            // { 
            // client.BaseAddress = new Uri("https://utilities.archesoftronix.in/");
            //    string responseContent = null;
            //    using (HttpResponseMessage response = await client.GetAsync("/api/GetCountry"))
            //    {
            //        responseContent = response.Content.ReadAsStringAsync().Result;
            //        response.EnsureSuccessStatusCode();
            //        var jsonResults = await client.GetStringAsync(client.BaseAddress);
            //        return Ok(responseContent);
            //    }
            //}

            //    var companyForCreation = new Country
            //    {
            //        pageNumber = 0,
            //        pageSize = 0,
            //        orderby = "true"
            //    };
            //    var company = JsonConvert.SerializeObject(companyForCreation);
            //    var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync(client.BaseAddress, requestContent);
            //    response.EnsureSuccessStatusCode();
            //    var content = await response.Content.ReadAsStringAsync();
            //    var createdCompany = JsonConvert.DeserializeObject< Country>(content);
            //    return Ok(createdCompany);
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
                // CountryGetModel objDeserializeObject = JsonConvert.DeserializeObject<CountryGetModel>(content);


                // CountryGetModel dataTable = JsonConvert.DeserializeObject<CountryGetModel>(content);


                CountryGetModel dataTable = new CountryGetModel();



                dataTable = (CountryGetModel)JsonConvert.DeserializeObject(content, (typeof(CountryGetModel)));


                // List<Data> models = JsonConvert.DeserializeObject<List<Data>>(content);

                // CountryGetModel dt = (CountryGetModel)JsonConvert.DeserializeObject(content, (typeof(CountryGetModel)));

                //var table = objDeserializeObject.data;



                return View(dataTable.data);
            }
        }

    }
}