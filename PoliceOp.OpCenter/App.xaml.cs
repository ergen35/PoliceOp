using RestSharp.Serializers.NewtonsoftJson;
using System.Windows;

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
