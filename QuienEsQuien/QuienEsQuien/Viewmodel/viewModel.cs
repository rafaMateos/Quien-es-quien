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

        private String _nickJugador;

        private ObservableCollection<ChatMessage> _msgsChat = new ObservableCollection<ChatMessage>();
        #endregion

        #region propiedades publicas
        public List<clsSala> listadoDeSalas {

            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public ObservableCollection<ChatMessage> msgsChats {

            get {

                return _msgsChat;

            }
            set {

                _msgsChat = value;
                NotifyPropertyChanged("msgsChats");
            }
        }

        public clsSala salaSeleccionada {

            get { return _salaSeleccionada; }

            set {

                _salaSeleccionada = value;
                Views.loby_screen.Position(_salaSeleccionada);

                //
               

            }
        }

        public String nickJugador {
            get { return _nickJugador; }
            set { _nickJugador = value; }
        }

        #endregion

        public viewModel() {

            ChatMessage m = new ChatMessage();
            m.groupName = "";
            m.message = "Hola";
            m.nickName = "dfdfd";

            _msgsChat.Add(m);
            rellenarListaSalasAsync();
            nickJugador = "Manolito Gafotas";
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

    }
}
