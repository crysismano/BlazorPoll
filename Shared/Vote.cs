using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared
{
    public class Vote
    {
        public int QuestionId { get; set; }
        public List<int> AnswerIds { get; set; }
    }
}
