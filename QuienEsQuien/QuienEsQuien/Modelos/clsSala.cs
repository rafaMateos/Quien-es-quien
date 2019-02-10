using System;
using System.Collections.Generic;
using System.Linq;


namespace QuienEsQuien.Modelos {
    public class clsSala {
        public int id { get; set; }
        public String nombre { get; set; }
        public int usuariosConectados { get; set; }

        public clsSala(int idSala, String nombreSala, int usuarios) {
            id = idSala;
            nombre = nombreSala;
            usuariosConectados = usuarios;
        }

        public clsSala() { }

    }
}