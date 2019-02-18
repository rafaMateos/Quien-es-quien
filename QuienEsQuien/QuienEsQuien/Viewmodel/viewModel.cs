using Manejadoras;
using QuienEsQuien.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace QuienEsQuien.Viewmodel {
    public class viewModel : clsBase {

        App myApp = (Application.Current as App);

        #region propiedades privadas
        private List<clsSala> _ListadoDeSalas;

        private clsSala _salaSeleccionada;

        public String salaActual;

        private String _nickJugador;
        private String _visibilidad;

        private ObservableCollection<ChatMessage> _msgsChat = new ObservableCollection<ChatMessage>();

        private List<clsCarta> _listadoDeCartas = new List<clsCarta>();

        private int _cartaGanadora;

        private clsCarta _cartaSeleccionada;

        private List<clsCarta> _listadoSecundarioDeCartas = new List<clsCarta>();

        private clsCarta _personajeGanador;

        private clsCarta _cartaGanadoraSeleccionada;

        private int _intentos = 0;

        #endregion
        #region propiedades publicas
        public List<clsSala> listadoDeSalas {
            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public ObservableCollection<ChatMessage> msgsChats {
            get { return _msgsChat; }
            set {
                _msgsChat = value;
                NotifyPropertyChanged("msgsChats");
            }
        }

        public clsSala salaSeleccionada {
            get { return _salaSeleccionada; }
            set {
                _salaSeleccionada = value;

                if (_salaSeleccionada != null) {

                    if (_salaSeleccionada.usuariosConectados < 2)
                    {

                        _visibilidad = "Visible";
                        NotifyPropertyChanged("visibilidad");
                        myApp.sala = _salaSeleccionada.nombre;
                        Views.lobby_screen.Position(_salaSeleccionada);
                    }
                    else
                    {

                        MostrarQueEsTonto();

                    }

                }
              
                
                /*
                myApp.sala = _salaSeleccionada.nombre;
                Views.lobby_screen.Position(_salaSeleccionada);*/
            }
        }

        private async void MostrarQueEsTonto()
        {

            //Cambiar esto porque peta
            ContentDialog noFunca = new ContentDialog();
            noFunca.Title = "No puedes conectarte a sala con 2 personas";
            noFunca.Content = "Eres tela de tonto";
            noFunca.PrimaryButtonText = "Salir";

            ContentDialogResult resultado = await noFunca.ShowAsync();

        }

        public String nickJugador {
            get { return _nickJugador; }
            set {
                _nickJugador = value;
                NotifyPropertyChanged("nickJugador");
            }
        }

        public List<clsCarta> listadoDeCartas {
            get { return _listadoDeCartas; }
            set { _listadoDeCartas = value; }
        }

        public List<clsCarta> listadoSecundarioDeCartas {
            get { return _listadoSecundarioDeCartas; }
            set { _listadoSecundarioDeCartas = value; }
        }

        public int cartaGanadora {
            get { return _cartaGanadora; }
            set { _cartaGanadora = value; }
        }

        public clsCarta cartaSeleccionada {
            get { return _cartaSeleccionada; }
            set {

                if (myApp.miTurno) {
                    _cartaSeleccionada = value;
                    listadoDeCartas[cartaSeleccionada.idCarta].estaBajada = !listadoDeCartas[cartaSeleccionada.idCarta].estaBajada;
                    NotifyPropertyChanged("cartaSeleccionada");
                    NotifyPropertyChanged("listadoDeCartas");
                    //petara aqui?
                    /*_cartaSeleccionada = null;
                    NotifyPropertyChanged("cartaSeleccionada");*/

                    listadoSecundarioDeCartas = new List<clsCarta>();

                    foreach (clsCarta cartita in listadoDeCartas) {
                        if (!cartita.estaBajada) {
                            listadoSecundarioDeCartas.Add(cartita);
                        }
                    }
                    NotifyPropertyChanged("listadoSecundarioDeCartas");
                }
                else {

                    //Nothing
                }
                
            }
        }

        public clsCarta personageGanador {

            get {

                return _personajeGanador;
            }
            set {

                _personajeGanador = value;
            }
        }

        public clsCarta cartaGanadoraSeleccionada {

            get {

                return _cartaGanadoraSeleccionada;
            }
            set {


                _cartaGanadoraSeleccionada = value;
            }
        }

        public string visibilidad {

            get {

                return _visibilidad;
            }
            set {

                _visibilidad = value;
                NotifyPropertyChanged("Visibilidad");
            }
        }

        public int intentos {

            get {

                return _intentos;
            }
            set {

                _intentos = value;
                NotifyPropertyChanged("intentos");
            }
        }
        #endregion

        public viewModel() {
            
            nickJugador = myApp.nickJugador;

            _visibilidad = "Collapsed";
            rellenarListaSalasAsync();
            rellenarListadoDeCartas();
            listadoSecundarioDeCartas = listadoDeCartas;
            anadirMensajesInicioChat();
        }

        private void anadirMensajesInicioChat() {
            ChatMessage m = new ChatMessage();
            m.groupName = "sala 1";
            m.nickName = "Sistema: ";
            m.message = " Bienvenido Usuario...";
            _msgsChat.Add(m);
            NotifyPropertyChanged("msgsChats");
        }

        /*public clsCarta ObtenerPersonajePartida() {

            Random random = new Random();
            clsCarta personaje;

            int randomPersonaje = random.Next(0, 23);

            personaje = _listadoDeCartas[randomPersonaje];

            return personaje;
            
        }*/


        public void AñadirAChat(ChatMessage c) {

            _msgsChat.Add(c);
            NotifyPropertyChanged("msgsChats");
        }

        public async Task rellenarListaSalasAsync() {

            clsManejadora manejadora = new clsManejadora();

            _ListadoDeSalas = await manejadora.GetSalas();
            NotifyPropertyChanged("listadoDeSalas");
        }

        public void rellenarListadoDeCartas() {
            listadoDeCartas.Add(new clsCarta(0, "Ángel", "../Assets/QS_angel.png"));
            listadoDeCartas.Add(new clsCarta(1, "Óscar", "../Assets/QS_oscar.png"));
            listadoDeCartas.Add(new clsCarta(2, "Fernando", "../Assets/QS_fernando.png"));
            listadoDeCartas.Add(new clsCarta(3, "Jorge", "../Assets/QS_jorge.png"));
            listadoDeCartas.Add(new clsCarta(4, "Dylan", "../Assets/QS_dylan.png"));
            listadoDeCartas.Add(new clsCarta(5, "Ángela", "../Assets/QS_angela.png"));
            listadoDeCartas.Add(new clsCarta(6, "Miguel Ángel", "../Assets/QS_miguelangel.png"));
            listadoDeCartas.Add(new clsCarta(7, "Nacho", "../Assets/QS_nacho.png"));
            listadoDeCartas.Add(new clsCarta(8, "Yeray", "../Assets/QS_yeray.png"));
            listadoDeCartas.Add(new clsCarta(9, "Rafa", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(10, "Asun", "../Assets/QS_asun.png"));
            listadoDeCartas.Add(new clsCarta(11, "1", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(12, "Luis", "../Assets/QS_luis.png"));
            listadoDeCartas.Add(new clsCarta(13, "2", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(14, "3", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(15, "Rosario", "../Assets/QS_rosario.png"));
            listadoDeCartas.Add(new clsCarta(16, "4", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(17, "José", "../Assets/QS_jose.png"));
            listadoDeCartas.Add(new clsCarta(18, "5", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(19, "Leo", "../Assets/QS_leo.png"));
            listadoDeCartas.Add(new clsCarta(20, "Sefran", "../Assets/QS_sefran.png"));
            listadoDeCartas.Add(new clsCarta(21, "6", "../Assets/QS_Rafa.png"));
            listadoDeCartas.Add(new clsCarta(22, "Vicky", "../Assets/QS_vicky.png"));
            listadoDeCartas.Add(new clsCarta(23, "Samuel", "../Assets/QS_samuel.png"));

            Random rnd = new Random();
            cartaGanadora = rnd.Next(0, 24);

            listadoDeCartas[cartaGanadora].esGanadora = true;
            _personajeGanador = listadoDeCartas[cartaGanadora];

            NotifyPropertyChanged("listadoDeCartas");
        }

        public DelegateCommand actualizarListadoSalas {
            get{
                return new DelegateCommand(actualizarListadoSalas_Executed);
            }
        }

        private async void actualizarListadoSalas_Executed() {
            rellenarListaSalasAsync();
        }
    }
}
