using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = "";
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [Required]
        public bool CorrectAnswer { get; set; } = false;
    }
}
