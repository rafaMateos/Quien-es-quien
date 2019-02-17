using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace divinaQuienSoyService.Models
{
    public class clsCarta {
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
              
            }
        }

        public string nombreCarta {
            get { return _nombreCarta; }
            set {
                _nombreCarta = value;
               
            }
        }

        public String imagenUri {
            get { return _imagenUri; }
            set {
                _imagenUri = value;
               
            }
        }

        public bool estaBajada {
            get { return _estaBajada; }
            set {
                _estaBajada = value;
               
            }
        }

        public bool esGanadora {
            get { return _esGanadora;  }
            set {
                _esGanadora = value;
              
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
