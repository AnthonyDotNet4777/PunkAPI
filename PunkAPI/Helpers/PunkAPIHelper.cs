using PunkAPI.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PunkAPI.Helpers
{
    public class PunkAPIHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Initialize()
        {
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["PunkBaseAddress"])
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<BeerObject>> GetBeerByName(String Name)
        {
            HttpResponseMessage response = await ApiClient.GetAsync("beers?beer_name=" + Name);
            IEnumerable<BeerObject> BeerList = null;

            if (response.IsSuccessStatusCode)
            {
                BeerList = await response.Content.ReadAsAsync<IEnumerable<BeerObject>>();
            }
            else
            {
                BeerList = null;
            }
            return BeerList;
        }

        public static async Task<Boolean> ValidateID(int id)
        {
            HttpResponseMessage response = await ApiClient.GetAsync("beers/" + id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}