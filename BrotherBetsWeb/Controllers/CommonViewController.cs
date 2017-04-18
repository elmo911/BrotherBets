using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Models;
using BrotherBetsWeb.Models;
using WebGrease.Css.Extensions;

namespace BrotherBetsWeb.Controllers
{
    public class CommonViewController : Controller
    {
        private BrotherManager _broManager;
        private BrotherManager BrotherManager => _broManager ?? (_broManager = new BrotherManager());
        private BookMaker _bookMaker;
        private BookMaker Bookie => _bookMaker ?? (_bookMaker = new BookMaker());


        public ActionResult BrotherSelectView()
        {
            var brothers = BrotherManager.GetAll();
            if(brothers == null || !brothers.Any())
                return PartialView("Error", "Cannot find the list of brothers");
            return PartialView(brothers);
        }

        public ActionResult BetPredictionList(int betId)
        {
            if (betId == default(int))
                return PartialView("Error", "Something went wrong while looking up predictions (Something was wired wrong)");
            try
            {
                var predictions = Bookie.GetPredictions(new Bet() { Id = betId });
                var groupedPredictions = new Dictionary<int, GroupedPrediction>();
                foreach (var prediction in predictions)
                {
                    var optionId = prediction.OutcomePredicted.Id;
                    if (!groupedPredictions.ContainsKey(optionId))
                    {
                        groupedPredictions.Add(optionId, new GroupedPrediction()
                        {
                            Answer = prediction.OutcomePredicted.Outcome,
                            Bets = new Dictionary<string, DateTime>() { { prediction.Bettor.Name, prediction.TimeOfPrediction } }
                        });
                    }
                    else
                        groupedPredictions[optionId].Bets.Add(prediction.Bettor.Name, prediction.TimeOfPrediction);
                }

                var totalVotes = groupedPredictions.Values.Sum(p => p.Bets.Count);
                groupedPredictions.Values.ForEach(p => p.PercentOfGuesses = (double)p.Bets.Count / totalVotes);
                ViewBag.Votes = totalVotes;
                return PartialView(groupedPredictions.Values.ToList());
            }
            catch (Exception)
            {
                return PartialView("Error", "Can't show predictions. (I Mathed too hard)");
            }
            
        }
    }
}