using BlazorPoll.Server.Data;
using BlazorPoll.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Server.Controllers
{
    [Route("polls")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PollController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Poll>>> GetPolls()
        {
            var result = await _context.Polls.Include(a => a.Questions).ThenInclude(a => a.Answers).ToListAsync();
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Poll>> CreatePoll(Poll poll)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Polls.AddAsync(poll);
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return Ok(await _context.Polls.Include(a => a.Questions).ThenInclude(a => a.Answers).ToListAsync());
        }
    }
}
