using Manejadoras;
using QuienEsQuien.Modelos;
using QuienEsQuien.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace QuienEsQuien
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Inicializa el objeto de aplicación Singleton. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        /// 

        public String sala = "";
        public String nickJugador = "";
        public bool esVolver = false;
        MediaPlayer player;
        public bool miTurno = false;
        clsManejadora maneja = new clsManejadora();

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            player = new MediaPlayer();
        }

        /// <summary>
        /// Se invoca cuando el usuario final inicia la aplicación normalmente. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, por ejemplo.
        /// </summary>
        /// <param name="e">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            //cargarMusikita();

            Frame rootFrame = Window.Current.Content as Frame;

            // No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
            // solo asegurarse de que la ventana está activa.
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Cuando no se restaura la pila de navegación, navegar a la primera página,
                    // configurando la nueva página pasándole la información requerida como
                    //parámetro de navegación
                    rootFrame.Navigate(typeof(home_screen), e.Arguments);
                }


                SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += App_CloseRequested;
                // Asegurarse de que la ventana actual está activa.
                Window.Current.Activate();
            }
        }

        private async void App_CloseRequested(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {

            if (this.sala.Equals("")) {

                App.Current.Exit();

            } else {

                ContentDialog noFunca = new ContentDialog();
                noFunca.Title = "¿Estas seguro de quieres salir?";
                noFunca.Content = "Vas a salir del juego..";
                noFunca.PrimaryButtonText = "Aceptar";
                noFunca.SecondaryButtonText = "Cancelar";
                ContentDialogResult result = await noFunca.ShowAsync();

                if (result == ContentDialogResult.Primary) {
                    game_screen salir = new game_screen();
                    clsSala sala = new clsSala();
                    sala.id = maneja.ObtenerIDSala(this.sala);
                    sala.nombre = this.sala;
                    sala.usuariosConectados = 0;

                    //Mostrar saliendo de sala
                    //salir.MostrarSalir();

                    await maneja.actualizarUsuariosSala(sala);
                    //LLamar a un metodo del serveer

                    salir.salirAbruptamente();

                    App.Current.Exit();

                }


            }


        }

        private async void cargarMusikita() {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("cancion.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
            player.Volume = 0.3;
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// </summary>
        /// <param name="sender">Marco que produjo el error de navegación</param>
        /// <param name="e">Detalles sobre el error de navegación</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación. El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles sobre la solicitud de suspensión.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }

        

         
    }
}
