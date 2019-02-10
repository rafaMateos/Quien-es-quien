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

        #endregion

        #region propiedades publicas
        public List<clsSala> listadoDeSalas {
            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public clsSala salaSeleccionada {
            get { return _salaSeleccionada; }
            set { _salaSeleccionada = value; }
        }

        public String nickJugador {
            get { return _nickJugador; }
            set { _nickJugador = value; }
        }

        #endregion

        public viewModel() {
            rellenarListaSalas();
            nickJugador = "Manolito Gafotas";
        }

        public void rellenarListaSalas() {

            _ListadoDeSalas = new List<clsSala>();

           clsSala sala1 = new clsSala(1, "sala 1",0);
            clsSala sala2 = new clsSala(2, "sala 2", 0);
            clsSala sala3 = new clsSala(3, "sala 3", 0);
            clsSala sala4 = new clsSala(4, "sala 4", 0);
            clsSala sala5 = new clsSala(5, "sala 5", 0);
            clsSala sala6 = new clsSala(6, "sala 6", 0);
            clsSala sala7 = new clsSala(7, "sala 7", 0);
            clsSala sala8 = new clsSala(8, "sala 8", 0);
            clsSala sala9 = new clsSala(9, "sala 9", 0);
            clsSala sala10 = new clsSala(10, "sala 10", 0);

            _ListadoDeSalas.Add(sala1);
            _ListadoDeSalas.Add(sala2);
            _ListadoDeSalas.Add(sala3);
            _ListadoDeSalas.Add(sala4);
            _ListadoDeSalas.Add(sala5);
            _ListadoDeSalas.Add(sala6);
            _ListadoDeSalas.Add(sala7);
            _ListadoDeSalas.Add(sala8);
            _ListadoDeSalas.Add(sala9);
            _ListadoDeSalas.Add(sala10);
        }

    }
}
