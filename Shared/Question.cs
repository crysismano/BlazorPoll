using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool SingleChoice { get; set; } = true;
        public ICollection<Answer> Answers { get; set; }
    }
}
