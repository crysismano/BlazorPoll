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
            await Clients.All.SendAsync("ReceivePoll", ActivePoll);
            await Clients.All.SendAsync("ReceiveQuestion", ActivePoll.Questions.FirstOrDefault());
        }

        public async Task GetNextQuestion()
        {
            if(_currentQuestionIdx + 1 < ActivePoll.Questions.Count )
            {
                _currentQuestionIdx++;
                await Clients.All.SendAsync("ReceiveQuestion", ActivePoll.Questions.ElementAtOrDefault(_currentQuestionIdx));
            }
        }

        public override async Task OnConnectedAsync()
        {
            if (ActivePoll is not null)
            {
                await Clients.Caller.SendAsync("ReceivePoll", ActivePoll);
                await Clients.Caller.SendAsync("ReceiveQuestion", ActivePoll.Questions.ElementAtOrDefault(_currentQuestionIdx));
            }
        }
    }
}
