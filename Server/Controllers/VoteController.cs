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
        private readonly IHubContext<PollHub> _hubContext;
        public VoteController(ApplicationDbContext context, IHubContext<PollHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
                    dbContextTransaction.Commit();
                    var question = await _context.Questions.Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == v.QuestionId);
                    await _hubContext.Clients.All.SendAsync("ReceiveVotes", question);
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
