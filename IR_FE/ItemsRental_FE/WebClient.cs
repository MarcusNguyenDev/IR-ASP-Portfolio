using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ItemsRental_FE
{
    public static class WebClient
    {
        public static HttpClient ApiClient = new HttpClient();

        /// <summary>
        /// This is the default constructor of this static class
        /// </summary>
        static WebClient()
        {
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]);
            ApiClient.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]);

        }


    }
}