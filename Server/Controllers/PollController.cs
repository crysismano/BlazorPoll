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
    [Route("api/[controller]")]
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
        [Route("[action]")]
        public async Task<ActionResult<Poll>> PostPoll([FromBody]Poll poll)
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
        [HttpPost]
        [Route("[action]")]
        public async Task PostVote([FromBody]Vote v)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var ai in v.AnswerIds)
                    {
                        await _context.Answers.Where(x => x.Id == ai).UpdateFromQueryAsync(x => new Answer { Votes = x.Votes + 1 });
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<Question>> GetQuestion(int questionId)
        {
            var question = await _context.Questions.Where(x => x.Id == questionId).Include(x => x.Answers).SingleOrDefaultAsync();
            if (question == null)
            {
                return NotFound();
            }
            return question;
        }
    }
}
