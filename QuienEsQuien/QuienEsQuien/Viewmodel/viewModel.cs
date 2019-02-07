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

        #endregion

    }
}
