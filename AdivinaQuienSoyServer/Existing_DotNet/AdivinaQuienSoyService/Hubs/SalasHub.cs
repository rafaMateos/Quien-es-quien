﻿using AdivinaQuienSoyService.Manejadora;
using AdivinaQuienSoyService.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdivinaQuienSoyService.Hubs
{

    [HubName("SalasHub")]
    public class SalasHub : Hub
    {

        private static int contadorUser = 0;
        private clsManejadora manejadora = new clsManejadora();
        /// <summary>
        /// Añade una conexion a una sala.
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>

        public async Task JoinRoomAsync(clsSala salita) {

            if (await manejadora.canUnirseSala(salita.id))
            {
                await Groups.Add(Context.ConnectionId, salita.nombre);
                salita.usuariosConectados++;
                manejadora.actualizarUsuariosSala(salita);
                Clients.All.ContarUsuariosAsync(salita);
            }

           
        }

        /// <summary>
        /// Elimina una conexion a una sala
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public Task LeaveRoom(String roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }


        /// <summary>
        /// Enviar mensaje a grupo
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <param name="sendTime"></param>
        public void SendToGroup(clsInfoChat info)
        {
            Clients.Group(info.groupName).ReceiveMessage(info.userName, info.Message);
        }


        //Tenemos esta info en la BD
        /*
        //Metodo que se ejecutara cada vez que haya una nueva conex
        public override Task OnConnected()
        {
            contadorUser++;
            Clients.All.usuarios(contadorUser);
            return base.OnConnected();
        }

        //Metodo que se ejecutara cada vez que se pierda una conex
        public override Task OnDisconnected(bool stopCalled)
        {
            contadorUser--;
            Clients.All.usuarios(contadorUser);
            return base.OnDisconnected(stopCalled);
        }
        */


    }
}