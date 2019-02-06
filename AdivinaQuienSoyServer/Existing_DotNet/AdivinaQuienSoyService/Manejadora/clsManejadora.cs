﻿using AdivinaQuienSoyService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AdivinaQuienSoyService.Manejadora
{
    public class clsManejadora
    {
        private String stringURIapi = "http://adivinaquiensoyapirest.azurewebsites.net/api/Salas";

        public async Task<Boolean> canUnirseSala(int idSala){
            Boolean veredicto = false;

            clsSala sala = new clsSala();
            Uri UriApi = new Uri($"{stringURIapi}/{idSala}");
            int Usuarios = -1;
            String res;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(UriApi);

            if (response.IsSuccessStatusCode){

                res = await response.Content.ReadAsStringAsync();
                sala = JsonConvert.DeserializeObject<clsSala>(res);
                Usuarios = sala.usuariosConectados;
            }

            if (Usuarios < 2){
                veredicto = true;
            }
            
            return veredicto;
        }

        /// <summary>
        /// Actualiza la sala que recibe por parámetros
        /// </summary>
        /// <param name="salita"></param>
        /// <returns></returns>
        public async Task<int> actualizarUsuariosSala(clsSala salita) {
            
            HttpClient mihttpClient = new HttpClient();
            String datos;
            int filas = -1;
            HttpContent contenido;
            Uri miUri = new Uri($"{stringURIapi}/{salita.id}");

            HttpResponseMessage miRespuesta = new HttpResponseMessage();
            try {
                datos = JsonConvert.SerializeObject(salita);

                contenido = new StringContent(datos, System.Text.Encoding.UTF8, "application/json");
                miRespuesta = await mihttpClient.PutAsync(miUri, contenido);

                if (miRespuesta.IsSuccessStatusCode) {
                    filas = 1;
                }
            } catch {
                //TODO
                filas = 0;
            }
            return filas;
        }

    }
}