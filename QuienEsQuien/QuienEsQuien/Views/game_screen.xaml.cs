using Manejadoras;
using Microsoft.AspNet.SignalR.Client;
using QuienEsQuien.Modelos;
using QuienEsQuien.Viewmodel;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Windows.UI.Xaml.Media.Animation;
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
        clsManejadora maneja = new clsManejadora();
        public static HubConnection conn { get; set; }
        public IHubProxy ChatProxy { get; set; }
        string sala;
        App myApp = (Application.Current as App);
        private CoreDispatcher _dispatcher;


        public game_screen() {

            this.InitializeComponent();

            Windows.UI.Core.Preview.SystemNavigationManagerPreview.GetForCurrentView().CloseRequested +=
               async (sender, args) => {
                   args.Handled = true;

                   if (!myApp.sala.Equals(""))
                   {


                       ContentDialog noFunca = new ContentDialog();
                       noFunca.Title = "¿Estas seguro de quieres salir?";
                       noFunca.Content = "Vas a salir del juego..";
                       noFunca.PrimaryButtonText = "Aceptar";
                       noFunca.SecondaryButtonText = "Cancelar";
                       ContentDialogResult result = await noFunca.ShowAsync();

                       if (result == ContentDialogResult.Primary)
                       {


                           clsSala sala = new clsSala();
                           sala.id = maneja.ObtenerIDSala(myApp.sala);
                           sala.nombre = myApp.sala;
                           sala.usuariosConectados = 0;
                           //Mostrar saliendo de sala
                           ActualizarApi(sala);
                           //LLamar a un metodo del serveer
                           salirAbruptamente();

                           App.Current.Exit();



                       }
                   }
                   else {

                       App.Current.Exit();
                   }





                   /*
                   await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {


                       salirDelJuego.Visibility = Visibility.Visible; //Cambiar aqui, cambiar desde el vm
                       int i = 0;
                       do {
                           Thread.Sleep(1000);
                           i++;
                       } while (i < 1);
                   });*/
               };



            _dispatcher = Window.Current.Dispatcher;
            vm = (viewModel)this.DataContext;
            SignalR();

            cargando();

            send.nickName = vm.nickJugador;
            send.groupName = sala;

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                ChatProxy.Invoke("JoinGroup", myApp.sala);
            }
            sumarmeAJugadoresConectados();
        }


        public async void ActualizarApi(clsSala salaActu) {

            clsManejadora maneja = new clsManejadora();
            await maneja.actualizarUsuariosSala(salaActu);
        }

        public async void NoMostrarSalir() {

                vm.visibilidadSalir = "Collapsed";

                int i = 0;
                do {
                    Thread.Sleep(1000);
                    i++;
                } while (i < 1);
           

        }
        private void SignalR() {
            //Connect to the url 
            conn = new HubConnection("https://adivinaquiensoy.azurewebsites.net/");
            //conn = new HubConnection("http://localhost:50268/");
            //ChatHub is the hub name defined in the host program. 

            ChatProxy = conn.CreateHubProxy("ChatHub");
            conn.Start();

            ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);
            ChatProxy.On("abandoPartida", volverLobby);
            ChatProxy.On("cambiarTurno", cambiarTurno);
            ChatProxy.On<clsCarta, string>("comprobarGanador", comprobarGanador);
            ChatProxy.On<string>("finalizarPartidaPorGanador", finalizarPartidaPorGanador);
            ChatProxy.On("falloAdivinar", actualizarIntentos);
            ChatProxy.On<string>("finalizarPartidaPorFallos", finalizarPartidaPorFallos);
            ChatProxy.On("salirAbriptamente", salirPorAbandono);
            ChatProxy.On("actualizarJugadoresConectados", actualizarJugadoresConectados);

            //ChatProxy.On<ChatMessage>("agregarMensaje", addMessage);
        }

        private async void salirPorAbandono() {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, async () => {
                    await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                        SalirAbruptuamenteRelative.Visibility = Visibility.Visible;

                        int i = 0;
                        do {
                            Thread.Sleep(1000);
                            i++;
                        } while (i < 1);
                    });
                });
        }
        
        private async void finalizarPartidaPorGanador(string obj) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, async () => {

                    await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                        nombreGanador.Text = obj;
                        ConfirmarGanador.Visibility = Visibility.Visible;
                    });

                    int i = 0;
                    do {
                        Thread.Sleep(1000);
                        i++;
                    } while (i < 1);
                });
        }

        private async void comprobarGanador(clsCarta obj, string nickname) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                if (vm.personageGanador.nombreCarta.Equals(obj.nombreCarta)) {

                    if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                        await ChatProxy.Invoke("Ganador", nickname, myApp.sala);
                    }
                } else {

                    if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                        await ChatProxy.Invoke("Perdedor", myApp.sala);

                        

                    }
                }
            });
        }

        public SolidColorBrush GetSolidColorBrush(string hex) {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        private async void actualizarIntentos() {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, async () => {


                    send.message = " " + myApp.nickJugador + " falló, se creia que eras " + vm.cartaGanadoraSeleccionada.nombreCarta;
                    send.groupName = myApp.sala;
                    send.nickName = "Sistema" + ": ";

                   
                    if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                    {
                        ChatProxy.Invoke("SendToGroup", send);
                    }

                    vm.intentos++;
                    var rojito = GetSolidColorBrush("#FFFF5252").Color;
                    var naranjita = GetSolidColorBrush("#FFFF9800").Color;
                    var amarillito = GetSolidColorBrush("#FFFFC107").Color;

                    switch (vm.intentos) {
                        case 1:
                            primerIntento.Fill = new SolidColorBrush(amarillito);
                            break;

                        case 2:
                            primerIntento.Fill = new SolidColorBrush(amarillito);
                            segundoIntento.Fill = new SolidColorBrush(naranjita);
                            break;

                        case 3:
                            primerIntento.Fill = new SolidColorBrush(amarillito);
                            segundoIntento.Fill = new SolidColorBrush(naranjita);
                            tercerIntento.Fill = new SolidColorBrush(rojito);
                            break;
                    }
                });

            if (vm.intentos == 3) {

                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                    await ChatProxy.Invoke("GanadorPorFallos", myApp.sala, myApp.nickJugador);
                }
            } else {
                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                    await ChatProxy.Invoke("pasarTurno", myApp.sala);
                }
            }
        }

        private async void finalizarPartidaPorFallos(string nickNamePerdedor) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                string textoInfo;

                if (nickNamePerdedor == myApp.nickJugador) {
                    textoInfo = "Perdiste, has fallado tres veces";
                    ActualizarUIPorFallos(textoInfo);
                } else {
                    textoInfo = $"Ganaste porque {nickNamePerdedor} falló tres veces.";
                    ActualizarUIPorFallos(textoInfo);
                }
            });
        }

        private async void ActualizarUIPorFallos(string textoInfo) {

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                TextoGandorOPerdedor.Text = textoInfo;
                ConfirmarGanadorPorFallos.Visibility = Visibility.Visible;
            });

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 1);


        }

        private async void cambiarTurno() {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                if (myApp.miTurno) {
                    myApp.miTurno = false;
                    turno.Text = "NO ES TU TURNO";
                } else {
                    myApp.miTurno = true;
                    turno.Text = "Es tu turno";
                }
            });
        }
        
        public async void ActualizarUi() {

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                ConfirmarGanador.Visibility = Visibility.Collapsed;
            });

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                ConfirmarGanadorPorFallos.Visibility = Visibility.Collapsed;
            });

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 1);

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Cargando.Visibility = Visibility.Visible;
            });
        }
        
        private async void volverLobby() {

            ActualizarUi();

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 3);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                myApp.esVolver = true;
                this.Frame.Navigate(typeof(lobby_screen));

            });
        }

        private async void cargando() {

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 10 || !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected));

            if (myApp.miTurno) {
                turno.Text = "Es tu turno";
            } else {
                turno.Text = "NO ES TU TURNO";
            }

            if (i == 10 && !(conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)) {
                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "¡Ups!";
                noFunca.Content = "Algo ha salido mal :'(";
                noFunca.PrimaryButtonText = "Salir";

                ContentDialogResult resultado = await noFunca.ShowAsync();

                if (resultado == ContentDialogResult.Primary) {
                    this.Frame.Navigate(typeof(login_screen));
                }
            }
        }

        private async void addMessage(ChatMessage obj) {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                vm.AñadirAChat(obj);
                contanierChat.ScrollIntoView(contanierChat.Items[contanierChat.Items.Count - 1]);

                contanierChat.SelectedItem = contanierChat.Items.Count - 1;
            });
        }

        private void Btn_Salir_Click(object sender, RoutedEventArgs e) {
            vm.visibilidad = "Visible";
            ChatProxy.Invoke("LeaveGroup", myApp.sala);

        }

        public void SalirPaSiempre() {
            ChatProxy.Invoke("LeaveGroup", myApp.sala);
        }

        private void Btn_Pasar_Click(object sender, RoutedEventArgs e) {

            if (myApp.miTurno) {
                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                    ChatProxy.Invoke("pasarTurno", myApp.sala);
                }
            }
        }

        public async void MostrarSalir() {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

                Cargando.Visibility = Visibility.Visible;
            });
        }

        public void salirAbruptamente() {

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 2);

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                ChatProxy.Invoke("SalirAbruptamente", myApp.sala);
            }
        }

        public void sumarmeAJugadoresConectados()
        {
            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                ChatProxy.Invoke("MandarInfoJugadoresConectados", myApp.sala);
            }
        }

        public async void actualizarJugadoresConectados()
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                int usuarios = await maneja.obtenerUsuariosSala(myApp.sala);
                ConnectedPlayers.Text = usuarios.ToString();
            });
        }

        private void Btn_send_Click(object sender, RoutedEventArgs e) {

            send.message = " " + tbx_chat.Text;
            send.groupName = myApp.sala;
            send.nickName = vm.nickJugador + ": ";

            if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {

                ChatProxy.Invoke("SendToGroup", send);
            }

            tbx_chat.Text = "";
        }

        private void Tbx_chat_KeyDown(object sender, KeyRoutedEventArgs e) {

            if (e.Key == Windows.System.VirtualKey.Enter) {

                send.message = " " + tbx_chat.Text;
                send.groupName = myApp.sala;
                send.nickName = vm.nickJugador + ": ";

                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                    ChatProxy.Invoke("SendToGroup", send);
                }
                tbx_chat.Text = "";
            }
        }

        private async void ConfirmarSeleccion_Click(object sender, RoutedEventArgs e) {

            if (myApp.miTurno) {
                if (conn.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected) {
                    ChatProxy.Invoke("sendPosibleWinner", vm.cartaGanadoraSeleccionada, myApp.sala, myApp.nickJugador);

                    /*
                    send.message = " " + myApp.nickJugador + " dice que tu personaje es...  " + vm.cartaGanadoraSeleccionada.nombreCarta;
                    send.groupName = myApp.sala;
                    send.nickName = "Sistema" + ": ";

                    ChatProxy.Invoke("SendToGroup", send);
                    */
                }
            } else {
                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "¡No!";
                noFunca.Content = "No puedes confirmar personaje si no es tu turno";
                noFunca.PrimaryButtonText = "Salir";

                ContentDialogResult resultado = await noFunca.ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ConfirmarGanador.Visibility = Visibility.Collapsed;
            ChatProxy.Invoke("LeaveGroup", myApp.sala);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            ConfirmarGanadorPorFallos.Visibility = Visibility.Collapsed;
            ChatProxy.Invoke("LeaveGroup", myApp.sala);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            SalirAbruptuamenteRelative.Visibility = Visibility.Collapsed;
            Cargando.Visibility = Visibility.Visible;

            int i = 0;
            do {
                Thread.Sleep(1000);
                i++;
            } while (i < 2);

            this.Frame.Navigate(typeof(lobby_screen));

        }

        private async void CartaSelectPanel_Tapped(object sender, TappedRoutedEventArgs e) {
            if (myApp.miTurno) {
               
                await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () => {
                    Storyboard myStory = new Storyboard();

                    RelativePanel clickedElement = sender as RelativePanel;
                    clsCarta miCarta = clickedElement.DataContext as clsCarta;

                    clsCarta miCartaV2 = vm.listadoDeCartas[miCarta.idCarta];

                    Object value = null;
                  
                      
                  

                    if (!miCartaV2.estaBajada) {

                        clickedElement?.Resources.TryGetValue("revelaImagen", out value);
                       

                    } else {

                        clickedElement?.Resources.TryGetValue("volteaImagen", out value);
                        
                    }

                    myStory = value as Storyboard;
                    myStory?.Begin();
                });


                
            }
        }

        private void GridImagenes_ItemClick(object sender, ItemClickEventArgs e) {

            //contanierChat.SelectedItem = contanierChat.Items.Count - 1;
            gridImagenes.SelectedItem = -1;


        }
    }
}
