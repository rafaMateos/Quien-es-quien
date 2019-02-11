using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdivinaQuienSoyService.Models
{
    public class ChatMessage
    {
        public string groupName { get; set; }
        public string nickName { get; set; }
        public string message { get; set; }
    }
}