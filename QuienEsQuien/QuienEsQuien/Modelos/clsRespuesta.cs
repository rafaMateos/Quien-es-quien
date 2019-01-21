using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Modelos
{
    public class clsRespuesta
    {

        #region Propiedades privadas
        private int _intentos;
        private int _cartasBajadas;
        #endregion

        #region Propiedades Publicas
        public int Intentos{
            get {

                return _intentos;
            }
            set {

                _intentos = value;
            }
        }
        public int CartasBajadas {

            get {

                return _cartasBajadas;

            }
            set {

                _cartasBajadas = value;
            }
        }
        #endregion



    }
}
