using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Modelos {
    public class clsCarta {
        #region Propiedades privadas
        private String _imagenUri;
        private bool _estaBajada;
        private bool _esGanadora;
        #endregion

        #region Propiedades publicas
        public String imagenUri {
            get { return _imagenUri; }
            set {  _imagenUri = value; }
        }

        public bool estaBajada {
            get {  return _estaBajada }
            set {  _estaBajada = value; }
        }

        public bool esGanadora {
            get { return _esGanadora;  }
            set { _esGanadora = value; }
        }
        #endregion Propiedades publicas
    }
}
