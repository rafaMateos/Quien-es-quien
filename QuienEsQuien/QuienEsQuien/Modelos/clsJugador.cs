using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuienEsQuien.Modelos
{
    public class clsJugador
    {
        #region Propiedades privadas
        private String _nickname;
        private int _id;
        private clsRespuesta _respuestas;
        private bool esGanador;
        #endregion

        #region Propiedades publicas
        public String NickName {

            get {

                return _nickname;
            }
            set {

                _nickname = value;
            }

        }
        public int Id {

            get {

                return _id;
            }
            set {

                _id = value;
            }
        }
        public clsRespuesta Respuestas {

            get {

                return _respuestas;

            }
            set {

                _respuestas = value;
            }
        }


        #endregion

    }
}
