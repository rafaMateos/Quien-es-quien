﻿using Manejadoras;
using QuienEsQuien.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace QuienEsQuien.Viewmodel {
    public class viewModel : clsBase {

        App myApp = (Application.Current as App);
        #region propiedades privadas
        private List<clsSala> _ListadoDeSalas;

        private clsSala _salaSeleccionada;

        public String salaActual;

        private String _nickJugador;

        private ObservableCollection<ChatMessage> _msgsChat = new ObservableCollection<ChatMessage>();

        private List<clsCarta> _listadoDeCartas = new List<clsCarta>();
        #endregion

        #region propiedades publicas
        public List<clsSala> listadoDeSalas {

            get { return _ListadoDeSalas; }
            set { _ListadoDeSalas = value; }
        }

        public ObservableCollection<ChatMessage> msgsChats {

            get {  return _msgsChat; }
            set { 
                _msgsChat = value;
                NotifyPropertyChanged("msgsChats");
            }
        }

        public clsSala salaSeleccionada {

            get { return _salaSeleccionada; } 
            set { 
                _salaSeleccionada = value;
                myApp.sala = _salaSeleccionada.nombre;
                Views.lobby_screen.Position(_salaSeleccionada); 
            }
        }

        public String nickJugador {
            get { return _nickJugador; }
            set {
                _nickJugador = value;
                NotifyPropertyChanged("nickJugador");
            }
        }

        public List<clsCarta> listadoDeCartas {
            get { return _listadoDeCartas; }
            set { _listadoDeCartas = value; }
        }

        #endregion

        public viewModel() {

            ChatMessage m = new ChatMessage();
            m.groupName = "";
            m.message = "Hola";
            m.nickName = "dfdfd";

            _msgsChat.Add(m);
            rellenarListaSalasAsync();
            rellenarListadoDeCartas();
        }

        public void AñadirAChat(ChatMessage c) {

            _msgsChat.Add(c);
            NotifyPropertyChanged("msgsChats");
        }

        public async void rellenarListaSalasAsync() {

            clsManejadora manejadora = new clsManejadora();

            _ListadoDeSalas = await manejadora.GetSalas();
            NotifyPropertyChanged("listadoDeSalas"); 
        }

        public void rellenarListadoDeCartas() {
            listadoDeCartas.Add(new clsCarta(0,"Ängel",     "../Assets/QS_angel.jpg"));
            listadoDeCartas.Add(new clsCarta(1,"Óscar",     "../Assets/QS_oscar.jpg"));
            listadoDeCartas.Add(new clsCarta(2,"Fernando",  "../Assets/QS_fernando.jpg"));
            listadoDeCartas.Add(new clsCarta(3,"Jorge",     "../Assets/QS_jorge.jpg"));
            listadoDeCartas.Add(new clsCarta(4,"-",     "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(5,"Ángela",    "../Assets/angela.jpg"));
            listadoDeCartas.Add(new clsCarta(6,"-",     "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(7,"Nacho",     "../Assets/QS_nacho.jpg"));
            listadoDeCartas.Add(new clsCarta(8,"Yeray",     "../Assets/QS_yerai.jpg"));
            listadoDeCartas.Add(new clsCarta(9,"Rafa",      "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(10,"Asun",     "../Assets/QS_asun.jpg"));
            listadoDeCartas.Add(new clsCarta(11,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(12,"Luis",     "../Assets/QS_luis.jpg"));
            listadoDeCartas.Add(new clsCarta(13,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(14,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(15,"Rosario",  "../Assets/QS_rosario.jpg"));
            listadoDeCartas.Add(new clsCarta(16,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(17,"José",     "../Assets/QS_jose.jpg"));
            listadoDeCartas.Add(new clsCarta(18,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(19,"Leo",      "../Assets/QS_leo.jpg"));
            listadoDeCartas.Add(new clsCarta(20,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(21,"-",    "../Assets/QS_Rafa.jpg"));
            listadoDeCartas.Add(new clsCarta(22,"Vicky",    "../Assets/QS_vicky.jpg"));
            listadoDeCartas.Add(new clsCarta(23,"Samuel",   "../Assets/QS_samuel.jpg"));
        }
    }
}
