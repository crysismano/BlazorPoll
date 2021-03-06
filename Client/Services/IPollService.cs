using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public interface IPollService
    {
        Task<List<Poll>> GetPolls();
        Task CreatePoll(Poll poll);
        Task CastVote(int questionId, List<int> answerIds);
        Task<Question> GetQuestion(int questionId);
        Task<Poll> GetPoll(int pollId);
        Task RemovePoll(int pollId);
        Task UpdatePoll(Poll poll);
    }
}
