using BlazorPoll.Client.Pages;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared;
using Microsoft.AspNetCore.Mvc;
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
        }
    }
}
