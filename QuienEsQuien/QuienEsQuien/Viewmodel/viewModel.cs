using Manejadoras;
using QuienEsQuien.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Viewmodel {
    public class viewModel : clsBase {

        #region propiedades privadas
        private List<clsSala> _ListadoDeSalas;

        private clsSala _salaSeleccionada;

        public String salaActual;

        private String _nickJugador;

        private ObservableCollection<ChatMessage> _msgsChat = new ObservableCollection<ChatMessage>();

        private List<clsCarta> _listadoDeCartas;
        #endregion

        #region propiedades publicas
        public List<clsSala> listadoDeSalas {

            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public ObservableCollection<ChatMessage> msgsChats {

            get {  return _msgsChat; }
            set { 
                _msgsChat = value;
                NotifyPropertyChanged("msgsChats");
            }
        }

        public clsSala salaSeleccionada {

            get { return _salaSeleccionada; } 
            set { 
                _salaSeleccionada = value; 
                Views.lobby_screen.Position(_salaSeleccionada); 
            }
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

        #endregion

        public viewModel() {

            ChatMessage m = new ChatMessage();
            m.groupName = "";
            m.message = "Hola";
            m.nickName = "dfdfd";

            _msgsChat.Add(m);
            rellenarListaSalasAsync();
        }

        public void AñadirAChat(ChatMessage c) {

            _msgsChat.Add(c);
            NotifyPropertyChanged("msgsChats");
        }

        public async void rellenarListaSalasAsync() {

            clsManejadora manejadora = new clsManejadora();

            _ListadoDeSalas = await manejadora.GetSalas();
            NotifyPropertyChanged("listadoDeSalas"); 
        }

        public void rellenarListadoDeCartas() {
            listadoDeCartas.Add(new clsCarta(0,"",""));
            listadoDeCartas.Add(new clsCarta(1,"",""));
            listadoDeCartas.Add(new clsCarta(2,"",""));
            listadoDeCartas.Add(new clsCarta(3,"",""));
            listadoDeCartas.Add(new clsCarta(4,"",""));
            listadoDeCartas.Add(new clsCarta(5,"",""));
            listadoDeCartas.Add(new clsCarta(6,"",""));
            listadoDeCartas.Add(new clsCarta(7,"",""));
            listadoDeCartas.Add(new clsCarta(8,"",""));
            listadoDeCartas.Add(new clsCarta(9,"",""));
            listadoDeCartas.Add(new clsCarta(10,"",""));
            listadoDeCartas.Add(new clsCarta(11,"",""));
            listadoDeCartas.Add(new clsCarta(12,"",""));
            listadoDeCartas.Add(new clsCarta(13,"",""));
            listadoDeCartas.Add(new clsCarta(14,"",""));
            listadoDeCartas.Add(new clsCarta(15,"",""));
            listadoDeCartas.Add(new clsCarta(16,"",""));
            listadoDeCartas.Add(new clsCarta(17,"",""));
            listadoDeCartas.Add(new clsCarta(18,"",""));
            listadoDeCartas.Add(new clsCarta(19,"",""));
            listadoDeCartas.Add(new clsCarta(20,"",""));
            listadoDeCartas.Add(new clsCarta(21,"",""));
            listadoDeCartas.Add(new clsCarta(22,"",""));
            listadoDeCartas.Add(new clsCarta(23,"",""));
        }
    }
}
