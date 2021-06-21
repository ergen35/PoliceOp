using RestSharp;
using System.Configuration;


namespace PoliceOp.OpCenter.AppLevel
{
    public static class APIClients
    {
        public static RestClient AppRestClient1 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
        public static RestClient AppRestClient2 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
        public static RestClient AppRestClient3 = new RestClient(ConfigurationManager.AppSettings["v1APIUrl"].ToString());
    }
}
