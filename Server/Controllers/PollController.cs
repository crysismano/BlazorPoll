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
            var result = await _context.Polls
                .Include(a => a.Questions)
                .ThenInclude(a => a.Answers)
                .AsSplitQuery()
                .ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task PostPoll([FromBody]Poll poll)
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
        }
        [HttpPut]
        [Route("[action]")]
        public async Task PutVote([FromBody]Vote v)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var ai in v.AnswerIds)
                    {
                        await _context.Answers.Where(x => x.Id == ai)
                            .UpdateFromQueryAsync(x => new Answer { Votes = x.Votes + 1 });
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        [HttpGet]
        [Route("[action]/{questionId}")]
        public async Task<ActionResult<Question>> GetQuestion(int questionId)
        {
            var question = await _context.Questions
                .Where(x => x.Id == questionId)
                .Include(x => x.Answers)
                .AsSplitQuery()
                .SingleOrDefaultAsync();
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpGet]
        [Route("[action]/{pollId}")]
        public async Task<ActionResult<Poll>> GetPoll(int pollId)
        {
            var poll = await _context.Polls.Where(x => x.Id == pollId).Include(x => x.Questions).ThenInclude(x => x.Answers).AsSplitQuery().SingleOrDefaultAsync();
            if(poll == null)
            {
                return NotFound();
            }
            return Ok(poll);
        }

        [HttpDelete("{pollId}")]
        public async Task DeletePoll(int pollId)
        {
            var poll = await _context.Polls.Where(x => x.Id == pollId).SingleOrDefaultAsync();
            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task UpdatePoll([FromBody]Poll poll)
        {
            var questions = poll.Questions;
            _context.Update(poll);
            await _context.SaveChangesAsync();
            _context.Entry(poll).State = EntityState.Detached;
            var dbPoll = await _context.Polls.Where(x => x.Id == poll.Id).Include(x => x.Questions).ThenInclude(x => x.Answers).AsSplitQuery().SingleOrDefaultAsync();
            var questionsToRemove = dbPoll.Questions.Where(x => !questions.Any(y => y.Id == x.Id)).ToList();
            _context.Questions.RemoveRange(questionsToRemove);
            await _context.SaveChangesAsync();
            var answersToKeep = new List<Answer>();
            foreach(var q in poll.Questions)
            {
                answersToKeep = answersToKeep.Concat(q.Answers).ToList();
            } 
            var allAnswers = new List<Answer>();
            foreach(var q in dbPoll.Questions)
            {
                allAnswers = allAnswers.Concat(q.Answers).ToList();
            }
            var answersToRemove = allAnswers.Where(x => !answersToKeep.Any(y => y.Id == x.Id)).ToList();
            _context.Answers.RemoveRange(answersToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
