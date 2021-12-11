﻿using BlazorPoll.Client.Pages;
using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient _httpClient;
        public VoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CastVote(int questionId, List<int> answerIds)
        {
            await _httpClient.PostAsJsonAsync("vote", new Vote {QuestionId = questionId, AnswerIds = answerIds });
        }
    }
}
