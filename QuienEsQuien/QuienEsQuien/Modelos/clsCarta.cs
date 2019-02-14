using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Modelos {
    public class clsCarta : clsBase {
        #region Propiedades privadas
        private int _idCarta;
        private string _nombreCarta;
        private String _imagenUri;
        private bool _estaBajada;
        private bool _esGanadora;
        #endregion

        #region Propiedades publicas

        public int idCarta{
            get { return _idCarta;  }
            set {
                _idCarta = value;
                NotifyPropertyChanged("idCarta");
            }
        }

        public string nombreCarta {
            get { return _nombreCarta; }
            set {
                _nombreCarta = value;
                NotifyPropertyChanged("nombreCarta");
            }
        }

        public String imagenUri {
            get { return _imagenUri; }
            set {
                _imagenUri = value;
                NotifyPropertyChanged("imagenUri");
            }
        }

        public bool estaBajada {
            get { return _estaBajada; }
            set {
                _estaBajada = value;
                NotifyPropertyChanged("estaBajada");
            }
        }

        public bool esGanadora {
            get { return _esGanadora;  }
            set {
                _esGanadora = value;
                NotifyPropertyChanged("esGanadora");
            }
        }
        #endregion Propiedades publicas

        public clsCarta( int idCarta_, string nombreCarta_, string imagenUri_ ) {

            idCarta = idCarta_;
            nombreCarta = nombreCarta_;
            imagenUri = imagenUri_;

            estaBajada = false;
            esGanadora = false;
        }
    }
}
