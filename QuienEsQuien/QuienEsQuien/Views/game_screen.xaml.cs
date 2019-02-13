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



    public sealed partial class game_screen : Page {

        ChatMessage send = new ChatMessage();
        viewModel vm = new viewModel();
        public static HubConnection conn { get; set; }
        public IHubProxy ChatProxy { get; set; }
        string sala;
        App myApp = (Application.Current as App);


      


        public game_screen() {

            this.InitializeComponent();
            vm = (viewModel)this.DataContext;
            SignalR();

            cargando();

            send.nickName = vm.nickJugador;
            send.groupName = sala;

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected ) {

                ChatProxy.Invoke("JoinGroup", myApp.sala);

            }

        }


        private void SignalR() {


            //Connect to the url 
            //conn = new HubConnection("https://adivinaquiensoy.azurewebsites.net/");
            conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 

            ChatProxy = conn.CreateHubProxy("ChatHub");
            conn.Start();

            ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);
            ChatProxy.On("abandoPartida", volverLobby);
           
            //ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);

        }

        private async void volverLobby() {


            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                myApp.esVolver = true;

                this.Frame.Navigate(typeof(lobby_screen));


            });

           
        }

        private async void cargando()
        {
            int i=0;
            do {
                Thread.Sleep(1000);
                i++;
            }while (i<10 || !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected));

           

            if (i==10 && !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected))
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

        private async void addMessage(ChatMessage obj) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                vm.AñadirAChat(obj);

            });

        }

        private void Btn_send_Click(object sender, RoutedEventArgs e) {

          

            send.message = tbx_chat.Text;
            send.groupName = myApp.sala;
            send.nickName = vm.nickJugador;

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                ChatProxy.Invoke("SendToGroup", send);
            }
            tbx_chat.Text = "";
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e) {

            ChatProxy.Invoke("LeaveGroup", myApp.sala);
           

        }


    }
}
