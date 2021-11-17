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
    [Route("questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions()
        {
            var result = await _context.Questions.Include(a => a.Answers).ToListAsync();
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(List<Question> questions)
        {
            using(var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var q in questions)
                    {
                        _context.Questions.Add(q);
                    }
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return Ok(await _context.Questions.Include(a => a.Answers).ToListAsync());
        }
    }
}
