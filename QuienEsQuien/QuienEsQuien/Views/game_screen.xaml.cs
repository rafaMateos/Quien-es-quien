using Microsoft.AspNet.SignalR.Client;
using QuienEsQuien.Modelos;
using QuienEsQuien.Viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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


        protected override void OnNavigatedTo(NavigationEventArgs e) {

            base.OnNavigatedTo(e);
            sala = (String)e.Parameter;
        }


        public game_screen() {
            this.InitializeComponent();
            vm = (viewModel)this.DataContext;
            SignalR();

            send.nickName = vm.nickJugador;
            send.groupName = sala;
 
        }


       

        private void SignalR() {





            //Connect to the url 
            //conn = new HubConnection("https://parejasdecartasnervion.azurewebsites.net/");
            conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 

            ChatProxy = conn.CreateHubProxy("ChatHub");
            conn.Start();

            ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);
            //ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);

        }

        private async void addMessage(ChatMessage obj) {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                vm.AñadirAChat(obj);
            });

        }

        private void Btn_send_Click(object sender, RoutedEventArgs e) {
            
            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                ChatProxy.Invoke("JoinGroup", send.groupName);

            }

            send.message = tbx_chat.Text;



            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                ChatProxy.Invoke("SendToGroup", send);
            }
        }
    }
}
