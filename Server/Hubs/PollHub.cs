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
        private static bool _isShowingResult;
        private static Poll ActivePoll { get; set; }
        
        public async Task SendPoll(Poll poll)
        {
            _isShowingResult = false;
            _currentQuestionIdx = 0;
            ActivePoll = poll;
            await Clients.All.SendAsync("ReceivePoll", ActivePoll, _currentQuestionIdx);
            await Clients.All.SendAsync("ReceiveQuestion", ActivePoll.Questions.FirstOrDefault());
        }

        public async Task GetNextQuestion()
        {
            if(_currentQuestionIdx + 1 < ActivePoll.Questions.Count )
            {
                _isShowingResult = false;
                _currentQuestionIdx++;
                await Clients.All.SendAsync("ReceiveQuestion", ActivePoll.Questions.ElementAtOrDefault(_currentQuestionIdx));
            }
        }

        public async Task ShowResult()
        {
            _isShowingResult = true;
            await Clients.All.SendAsync("ShowResult");
        }

        public async Task SendVote()
        {
            await Clients.All.SendAsync("Update");
        }

        public override async Task OnConnectedAsync()
        {
            if (ActivePoll is not null)
            {
                await Clients.Caller.SendAsync("Update");
                await Clients.Caller.SendAsync("ReceivePoll", ActivePoll, _currentQuestionIdx);
                await Clients.Caller.SendAsync("ReceiveQuestion", ActivePoll.Questions.ElementAtOrDefault(_currentQuestionIdx));
                if (_isShowingResult)
                {
                    await Clients.Caller.SendAsync("ShowResult");
                }
            }
        }
    }
}
