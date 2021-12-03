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
        private static int _currentQuestionIdx;
        public static Poll ActivePoll { get; set; }
        
        public async Task SendPoll(Poll poll)
        {
            _currentQuestionIdx = 0;
            ActivePoll = poll;
            await Clients.All.SendAsync("ReceiveQuestion", ActivePoll.Questions.FirstOrDefault());
        }

        public override async Task OnConnectedAsync()
        {
            if (ActivePoll is not null)
            {
                await Clients.Caller.SendAsync("ReceiveQuestion", ActivePoll.Questions.FirstOrDefault());
            }
        }
    }
}
