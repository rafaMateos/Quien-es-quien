using AdivinaQuienSoyService.Manejadora;
using AdivinaQuienSoyService.Models;
using divinaQuienSoyService.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdivinaQuienSoyService.Hubs
{
    public class ChatHub : Hub
    {

        clsManejadora maneja = new clsManejadora();
        int id = 0;
        /// <summary>
        /// Añade una conexion a un grupo
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);

        }

        public void pasarTurno(String groupName) {

            Clients.Group(groupName).cambiarTurno();
        }

        public void sendPosibleWinner(clsCarta carta, string grupo,string nickname) {

            Clients.Group(grupo,Context.ConnectionId).comprobarGanador(carta,nickname);//vaya sacada de polla v2 by dylan nene

          
        }

        public void Ganador(string nickname, string groupname) {

            Clients.Group(groupname).finalizarPartidaPorGanador(nickname);
        }


        public void Perdedor(string grupo) {

            Clients.Group(grupo,Context.ConnectionId).falloAdivinar();
        }

       

        /// <summary>
        /// Elimina una conexion del grupo
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public void LeaveGroup(string groupName)
        {
            Clients.Group(groupName).abandoPartida();
            Groups.Remove(Context.ConnectionId, groupName);  

        }
        /// <summary>
        /// Envia el mensaje a los clientes conectados al grupo
        /// </summary>
        /// <param name="message">ChatMessage</param>
        public void SendToGroup(ChatMessage message)
        {
            Clients.Group(message.groupName).agregarMensaje(message);
        }

       
    }
}