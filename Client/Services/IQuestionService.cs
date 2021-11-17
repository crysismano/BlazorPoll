using BlazorPoll.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Client.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestions();
        Task<List<Question>> CreateQuestion(List<Question> questions);
    }
}
