using Manejadoras;
using QuienEsQuien.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Viewmodel {
    public class viewModel : clsBase {

        #region propiedades privadas
        private List<clsSala> _ListadoDeSalas;

        private clsSala _salaSeleccionada;

        private String _nickJugador;

        private List<ChatMessage> _msgsChat;
        #endregion

        #region propiedades publicas
        public List<clsSala> listadoDeSalas {

            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public List<ChatMessage> msgChats {

            get {

                return _msgsChat;

            }
            set {

                _msgsChat = value;
                NotifyPropertyChanged("msgChats");
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
            rellenarListaSalasAsync();
            nickJugador = "Manolito Gafotas";
        }

        public async void rellenarListaSalasAsync() {

            clsManejadora manejadora = new clsManejadora();

            _ListadoDeSalas = await manejadora.GetSalas();
            NotifyPropertyChanged("listadoDeSalas");
            
         
        }

    }
}
