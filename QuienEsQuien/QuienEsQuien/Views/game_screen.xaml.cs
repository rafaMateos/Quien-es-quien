using Microsoft.AspNet.SignalR.Client;
using QuienEsQuien.Modelos;
using QuienEsQuien.Viewmodel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QuienEsQuien.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
  
    

    public sealed partial class game_screen : Page
    {

        viewModel vm = new viewModel();
        public static HubConnection conn { get; set; }
        public IHubProxy ChatProxy { get; set; }


        public game_screen()
        {
            this.InitializeComponent();
            SignalR();

            //recoger aqui el objeto sala del navigate to y el nick name
            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                ChatProxy.Invoke("JoinGroup", "Sala 1");

            }

        }



        private void SignalR()
        {

            //Connect to the url 
            //conn = new HubConnection("https://parejasdecartasnervion.azurewebsites.net/");
            conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 
         
            ChatProxy = conn.CreateHubProxy("ChatHub");
            conn.Start();

            ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);
            //ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);

        }

        private async void addMessage(ChatMessage obj)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                vm.msgChats.Add(obj);
            });

        }

        private void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            ChatMessage send = new ChatMessage();
            send.Message = tbx_chat.Text;
            send.Username = "Juan";
            send.groupName = "sala 1";
            

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                ChatProxy.Invoke("SendToGroup", send);
            }


        }
    }
}
