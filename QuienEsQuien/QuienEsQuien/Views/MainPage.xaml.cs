using Manejadoras;
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
            //conn = new HubConnection("https://parejasdecartasnervion.azurewebsites.net/");
            conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 
            MyHubProxy = conn.CreateHubProxy("SalasHub");
            conn.Start();

            MyHubProxy.On<clsSala>("ContarUsuarios", onInfo);



        }

        //aqui lo mandaremos al server y lo conectaremos e avisaremos a todos los usuarios de que esta conectado.
        public static void Position(clsSala info)
        {

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                info.id = 1;
                info.nombre = "sala 1";
                info.usuariosConectados = 0;
                MyHubProxy.Invoke("JoinRoomAsync", info);

            }

        }

        //ya llega aqui al darle click del server que se ha conectado. debemos controlar qe se pueda o no conectar aqui
        private async void onInfo(clsSala obj)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {

                //Actualizar con la info que llega del server en el objeto clsSala obj



            });
        }

        private async void ButtonJoin_Click(object sender, RoutedEventArgs e)
        {

            //Hacer get a la sala de unirse
            clsSala info = new clsSala();
            info.id = 1;
            info.nombre = "sala 1";
            info.usuariosConectados = 0;

            clsManejadora manejadora = new clsManejadora();
            
           Boolean ret = await manejadora.canUnirseSala(info.id);

            if (ret)
            {

                Position(info);

            }
            else {

                //TODO
            }
            //Debemos llamar a la api, e introducir los datos de la sala para pedir la info y rellenar el objeto
            //vete al position
            
            
            


        }
    }
}
