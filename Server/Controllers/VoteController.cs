using BlazorPoll.Client.Pages;
using BlazorPoll.Server.Data;
using BlazorPoll.Server.Hubs;
using BlazorPoll.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public async Task CastVote(List<Vote> votes)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.AddRangeAsync(votes);
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            await _hubContext.Clients.All.SendAsync("ReceiveVotes", votes);
        }
    }
}
