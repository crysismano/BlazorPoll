using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared
{
    public class Vote
    {
        public int Id { get; set; }
        [Required]
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
