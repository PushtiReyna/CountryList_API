namespace CountryList.Models
{
    public class CountryData
    {
        public class CountryGetModel
        {
            public string message { get; set; }
            public bool status { get; set; }
            public Data[] data { get; set; }
            //public List<Data> datas { get; set; }
            public int code { get; set; }
        }

        public class Data
        {
            public int countryId { get; set; }
            public string countryName { get; set; }
            public string iso2 { get; set; }
            public string dialingCode { get; set; }
        }


    }
}
