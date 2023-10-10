using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CountryList.Models
{
    public class Country
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string orderby { get; set; }
        public int countryId { get; set; }
        public string countryNmae { get; set; }

        public string iso2 { get; set; }
        public string dialingCode { get; set; }

    }
    public class CountryReqModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
    }


}