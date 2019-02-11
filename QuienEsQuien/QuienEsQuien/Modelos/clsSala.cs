using System;
using System.Collections.Generic;
using System.Linq;


namespace QuienEsQuien.Modelos {
    public class clsSala : clsBase{
        public int id { get; set; }
        public String nombre { get; set; }
        private int _usuariosConectados;


        public int usuariosConectados {

            get{

                return _usuariosConectados;
            }
            set {

                _usuariosConectados = value;
                NotifyPropertyChanged("usuariosConectados");
            }


        }




        public clsSala(int idSala, String nombreSala, int usuarios) {
            id = idSala;
            nombre = nombreSala;
            usuariosConectados = usuarios;
        }

        public clsSala() { }

    }
}