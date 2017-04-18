using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrotherBetsWeb.Models
{
    public class GroupedPrediction
    {
        public string Answer { get; set; }
        public Dictionary<string, DateTime> Bets { get; set; }
        public double PercentOfGuesses { get; set; }
        public string PercentDisplay => PercentOfGuesses.ToString("P");
    }
}