using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrotherBetsLibrary.Models;

namespace BrotherBetsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var bets = new List<Bet>()
            {
                new Bet()
                {
                    Name = "Bet 1",
                    Expiration = DateTime.Today.AddDays(1)
                },
                new Bet()
                {
                    Name = "Bet 2",
                    Expiration = DateTime.Today.AddDays(7)
                },
            };

            return View(bets);
        }


        public ActionResult Guess(int id)
        {
            throw new NotImplementedException();
        }
    }
}