using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPoll.Server.Controllers
{
    public class VoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
