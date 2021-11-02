using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Utils
{
    public class Chat : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("newMessage", "anonymous", message);
        }
    }
}
