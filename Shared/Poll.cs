using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared
{
    public class Poll
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
