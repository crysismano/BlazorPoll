using BlazorPoll.Client.Pages;
using BlazorPoll.Server.Data;
using BlazorPoll.Server.Hubs;
using BlazorPoll.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Server.Controllers
{
    [Route("vote")]
    [ApiController]
    public class VoteController : Controller
    {

        private readonly ApplicationDbContext _context;
        public VoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task CastVote(Vote v)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach(var ai in v.AnswerIds)
                    {
                        await _context.Answers.Where(x => x.Id == ai).UpdateFromQueryAsync(x => new Answer { Votes = x.Votes +1 });
                    }
                    dbContextTransaction.Commit();                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<Question>>GetQuestion(int questionId)
        {
            var question = await _context.Questions.Where(x => x.Id == questionId).Include(x => x.Answers).SingleOrDefaultAsync();
            if(question == null)
            {
                return NotFound();
            }
            return question;
        }
    }
}
