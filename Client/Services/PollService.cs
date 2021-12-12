using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public class PollService : IPollService
    {
        private readonly HttpClient _httpClient;
        public PollService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePoll(Poll poll)
        {
            await _httpClient.PostAsJsonAsync("api/poll/PostPoll", poll);
        }

        public async Task<List<Poll>> GetPolls()
        {
            return await _httpClient.GetFromJsonAsync<List<Poll>>("api/poll");
        }

        public async Task CastVote(int questionId, List<int> answerIds)
        {
            await _httpClient.PostAsJsonAsync("api/poll/PostVote", new Vote { QuestionId = questionId, AnswerIds = answerIds });
        }

        public async Task<Question> GetQuestion(int questionId)
        {
            return await _httpClient.GetFromJsonAsync<Question>($"api/poll/{questionId}");
        }
    }
}
