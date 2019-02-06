using Microsoft.AspNet.SignalR.Client;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace QuienEsQuien
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static HubConnection conn { get; set; }
        public IHubProxy proxy { get; set; }
        public static IHubProxy MyHubProxy { get; set; }



        public MainPage()
        {
            this.InitializeComponent();
            SignalR();
        }

        private void SignalR()
        {

            //Connect to the url 
            conn = new HubConnection("https://parejasdecartasnervion.azurewebsites.net/");
            //conn = new HubConnection("http://localhost:58716/");
            //ChatHub is the hub name defined in the host program. 
            MyHubProxy = conn.CreateHubProxy("infoPartidaHub");
            conn.Start();

            MyHubProxy.On<clsSala>("sendInfoPartida", onInfo);



        }

        private void onInfo(clsSala obj)
        {
            


        }

        private async void ButtonJoin_Click(object sender, RoutedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {



            });

        }
    }
}
