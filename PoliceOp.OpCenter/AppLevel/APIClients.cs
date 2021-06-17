using RestSharp;
using System.Configuration;
using Tiny.RestClient;

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
