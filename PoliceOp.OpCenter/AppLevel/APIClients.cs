using System;
using System.Collections.Generic;
using System.Text;
using Tiny.RestClient;
using System.Configuration;
using RestSharp;

namespace PoliceOp.OpCenter.AppLevel
{
    public static class APIClients
    {
        public static TinyRestClient v1Client = new TinyRestClient(new System.Net.Http.HttpClient(), ConfigurationManager.AppSettings["v1APIUrl"].ToString());
        public static RestClient AppRestClient1 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
        public static RestClient AppRestClient2 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
        public static RestClient AppRestClient3 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
    }
}
