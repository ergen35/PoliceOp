using System;
using System.Collections.Generic;
using System.Text;
using Tiny.RestClient;
using System.Configuration;

namespace PoliceOp.OpCenter.AppLevel
{
    public static class APIClients
    {
        public static TinyRestClient v1Client = new TinyRestClient(new System.Net.Http.HttpClient(), ConfigurationManager.AppSettings["v1APIUrl"].ToString());
    }
}
