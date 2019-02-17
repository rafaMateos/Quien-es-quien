using Manejadoras;
using Microsoft.AspNet.SignalR.Client;
using QuienEsQuien.Modelos;
using QuienEsQuien.Viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QuienEsQuien.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 



    public sealed partial class lobby_screen : Page {

        public static HubConnection conn { get; set; }
        public IHubProxy ChatProxy { get; set; }
        public static IHubProxy SalasProxy { get; set; }

        viewModel miVM = new viewModel();
        Boolean estaSala = false;
         static App myApp = (Application.Current as App);
        string nick = "ERROR" ;

        int contadorCargando = 0;

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            base.OnNavigatedTo(e);
            miVM.nickJugador = myApp.nickJugador;

            if (!myApp.esVolver) {

               // miVM.nickJugador = (string)e.Parameter;

            } else {
                myApp.esVolver = false;

                //Llegamos hasta aqui
                cargando();

                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                    SalasProxy.Invoke("LeaveRoom", myApp.sala);
                    myApp.sala = "";

                }
            }
            //Aqui invokar al LeeveRoom segun el parametro que pasemo.



        }

        public lobby_screen() {
            this.InitializeComponent();
            SignalR();
            miVM = (viewModel)this.DataContext;
        }


        private void SignalR() {

            //Connect to the url 
            conn = new HubConnection("https://adivinaquiensoy.azurewebsites.net/");
            //conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 
            SalasProxy = conn.CreateHubProxy("SalasHub");
            //ChatProxy = conn.CreateHubProxy("ChatHub");
            conn.Start();

            SalasProxy.On<clsSala>("ContarUsuarios", onInfo);
            SalasProxy.On<string>("Descontar", onDescontar);
            SalasProxy.On<String>("PasarAJuego", PasarAJugar);
            //ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);

        }

        //Aqui descontaremos igual que en onInfo 
        private async void onDescontar(string sala) {

                cargando2();
            //¿Esto no lo hace si ponemos await????????
               
                await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                    int id = ObtenerIDSala(sala);

                    var salaEdit = (clsSala)listSalas.Items[id - 1];

                    salaEdit.usuariosConectados = salaEdit.usuariosConectados - 1;
                    clsManejadora manejadora = new clsManejadora();
                    manejadora.actualizarUsuariosSala(salaEdit);



                });

            


        }

        public int ObtenerIDSala(string nombre) {

            int id = 0;

            switch (nombre) {

                case "sala 1":
                    id = 1;
                    break;

                case "sala 2":
                    id = 2;
                    break;

                case "sala 3":
                    id = 3;
                    break;

                case "sala 4":
                    id = 4;
                    break;

                case "sala 5":
                    id = 5;
                    break;

                case "sala 6":
                    id = 6;
                    break;

                case "sala 7":
                    id = 7;
                    break;

                case "sala 8":
                    id = 8;
                    break;

                case "sala 9":
                    id = 9;
                    break;

                case "sala 10":
                    id = 10;
                    break;
            }

            return id;

        }

        private async void PasarAJugar(string salaNombre) {


            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                if (!myApp.sala.Equals("") && !estaSala) {

                    estaSala = true;
                    this.Frame.Navigate(typeof(game_screen));

                }
               

            });

        }



        public static async void Position(clsSala info) {

           

            if (info.usuariosConectados != 2)
            {

                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {

                    if (info.usuariosConectados == 0)
                    {
                        myApp.miTurno = true;
                    }
                    else
                    {
                        myApp.miTurno = false;
                    }

                    SalasProxy.Invoke("JoinRoomAsync", info);

                }

            }
            else {

                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "Sala llena";
                noFunca.Content = "Utilize otra sala para jugar";
                noFunca.PrimaryButtonText = "OK";
                ContentDialogResult resultado = await noFunca.ShowAsync();

                

            }
          

           
        }

        private async void cargando2()
        {
            
            int i = 0;
            do
            {
                Thread.Sleep(1000);
                i++;
            } while (i < 5);
            

            //Probar porque esto no actualiza y no entr<a en el onDescontar
            
            if (!(miVM.listadoDeSalas != null))
            {
                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "Error";
                noFunca.Content = "Ha ocurrido un fallo en la conexion :'(";
                noFunca.PrimaryButtonText = "Salir";

                ContentDialogResult resultado = await noFunca.ShowAsync();

                if (resultado == ContentDialogResult.Primary)
                {
                    this.Frame.Navigate(typeof(login_screen));
                }
            }
        }

        private async void cargando()
        {
            int i = 0;
            do
            {
                Thread.Sleep(1000);
                i++;
            } while (i < 10 || !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected));



            if (i == 10 && !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected))
            {
                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "Error";
                noFunca.Content = "Ha ocurrido un fallo en la conexion :'(";
                noFunca.PrimaryButtonText = "Salir";

                ContentDialogResult resultado = await noFunca.ShowAsync();

                if (resultado == ContentDialogResult.Primary)
                {
                    this.Frame.Navigate(typeof(login_screen));
                }
            }
        }

        /*private void addToGroup(string groupName)
        {
            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                ChatProxy.Invoke("JoinGroup", groupName);
            }
        }
        */

        private async void onInfo(clsSala obj) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                /*
                if (!myApp.sala.Equals("")) {
                    Cargando.Visibility = Visibility.Visible;
                }
                */
               
                clsManejadora manejadora = new clsManejadora();
                manejadora.actualizarUsuariosSala(obj);

                var sala = (clsSala)listSalas.Items[obj.id - 1];
                sala.usuariosConectados = obj.usuariosConectados;


            });


            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                SalasProxy.Invoke("PasarAsalaServer", obj.nombre);

            }


        }

    }
}
