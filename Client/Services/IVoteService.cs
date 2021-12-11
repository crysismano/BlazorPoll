using BlazorPoll.Client.Pages;
using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public interface IVoteService
    {
        public Task CastVote(int questionId, List<int> answerIds);
    }
}
