using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrotherBetsLibrary;

namespace BrotherBetsWeb.Controllers
{
    public class BettorApiController : Controller
    {
        private BettorManager _bettorManager;
        private BettorManager BettorManager => _bettorManager ?? (_bettorManager = new BettorManager());

        public JsonResult BettorNameAutoComplete(string term)
        {
            var bettors = BettorManager.BettorNamesLike(term);
            return Json(bettors, JsonRequestBehavior.AllowGet);
        }
    }
}