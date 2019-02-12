using QuienEsQuien.Viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class login_screen : Page {

        viewModel miVM = new viewModel();

        public login_screen() {
            this.InitializeComponent();
            miVM = (viewModel)this.DataContext;
        }

        private void HyperButton_Click(object sender, RoutedEventArgs e) {
            String nickelodeon = txtNickJugador.Text;
            this.Frame.Navigate(typeof(lobby_screen), nickelodeon);
        }
    }
}
