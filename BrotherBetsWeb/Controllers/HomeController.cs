using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Models;

namespace BrotherBetsWeb.Controllers
{
    public class HomeController : Controller
    {
        private BookMaker _bookMaker;
        private BookMaker Bookie => _bookMaker ?? (_bookMaker = new BookMaker());
        private BrotherManager _broManager;
        private BrotherManager BrotherManager => _broManager ?? (_broManager = new BrotherManager());
        private BettorManager _bettorManager;
        private BettorManager BettorManager => _bettorManager ?? (_bettorManager = new BettorManager());

        public ActionResult Index()
        {
            var bets = Bookie.Bets();

            return View(bets);
        }


        public ActionResult Guess(int id)
        {
            var bet = Bookie.GetBet(id);
            if (bet == null) return HttpNotFound();
            return View(bet);
        }

        [HttpPost]
        public ActionResult Guess(int betId, int outcomeId, string bettorName)
        {
            var bet = Bookie.GetBet(betId);
            if (bet == null) return View("Error");
            var outcome = bet.BetOptions.FirstOrDefault(betOption => betOption.Id == outcomeId);
            if (outcome == null) return View("Error");
            var brother = bet.Brother;
            if (brother == null) return View("Error");
            var bettor = BettorManager.Get(bettorName);
            if (bettor == null)
            {
                BettorManager.Add(bettorName);
                bettor = BettorManager.Get(bettorName);
            }
            try
            {
                if(Bookie.HasTakenBet(bettor, bet))
                    throw new Exception($"{bettor.Name} has already place a bet and cannot bet again.");
                Bookie.TakeBet(bettor, outcome, brother);
            }
            catch (Exception exception)
            {
                ViewBag.VotingError = exception.Message;
                return View(bet);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Bet newBet, string bettorName, string brotherName, string[] outcomes)
        {
            try
            {
                var brother = BrotherManager.Get(brotherName);
                var bettor = BettorManager.Get(bettorName);
                if (bettor == null)
                {
                    BettorManager.Add(bettorName);
                    bettor = BettorManager.Get(bettorName);
                }
                
                Bookie.AddBet(newBet, bettor, brother, outcomes);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View(newBet);
            }
            
            return RedirectToAction("Index");
        }
    }
}