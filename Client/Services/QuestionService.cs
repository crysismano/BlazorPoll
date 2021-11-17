using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;
        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Question>> CreateQuestion(List<Question> questions)
        {
            var result = await _httpClient.PostAsJsonAsync($"questions", questions);
            return await result.Content.ReadFromJsonAsync<List<Question>>();
        }

        public async Task<List<Question>> GetQuestions()
        {
            return await _httpClient.GetFromJsonAsync<List<Question>>("questions");
        }
    }
}
