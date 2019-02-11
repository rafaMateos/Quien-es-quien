using AdivinaQuienSoyService.Models;
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
        /// <summary>
        /// Añade una conexion a un grupo
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Task JoinGroup(string groupName)
        {

            return Groups.Add(Context.ConnectionId, groupName);
            //await Groups.Add(Context.ConnectionId, groupName);
            //Clients.Group(groupName).addChatMessage(Context.User.Identity.Name + " joined.");
        }

        /// <summary>
        /// Elimina una conexion del grupo
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
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