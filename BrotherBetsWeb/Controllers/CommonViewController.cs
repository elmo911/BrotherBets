using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrotherBetsLibrary;

namespace BrotherBetsWeb.Controllers
{
    public class CommonViewController : Controller
    {
        private BrotherManager _broManager;
        private BrotherManager BrotherManager => _broManager ?? (_broManager = new BrotherManager());

        public ActionResult BrotherSelectView()
        {
            var brothers = BrotherManager.GetAll();
            if(brothers == null || !brothers.Any())
                return PartialView("Error", "Cannot find the list of brothers");
            return PartialView(brothers);
        }
    }
}