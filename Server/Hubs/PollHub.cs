using BlazorPoll.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Server.Hubs
{
    public class PollHub : Hub
    {
        public Poll CurrentPoll { get; set; }
        public async Task SendPoll(Poll poll)
        {
            CurrentPoll = poll;
            await Clients.All.SendAsync("RecieveQuestion", poll);
        }
    }
}
