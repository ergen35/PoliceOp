using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RestSharp;
using PoliceOp.OpCenter.AppLevel;
using RestSharp.Serializers.NewtonsoftJson;

namespace PoliceOp.OpCenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppLevel.APIClients.AppRestClient1.UseNewtonsoftJson();
            AppLevel.APIClients.AppRestClient2.UseNewtonsoftJson();
            AppLevel.APIClients.AppRestClient3.UseNewtonsoftJson();

        }
    }
}
