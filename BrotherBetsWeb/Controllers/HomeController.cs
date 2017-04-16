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

        public ActionResult Index()
        {
            var bets = Bookie.GetBets();

            return View(bets);
        }


        public ActionResult Guess(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Bet newBet, string brother, string[] outcomes)
        {
            try
            {
                Bookie.AddBet(newBet, BrotherManager.Get(brother), outcomes);
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