using System;

namespace BrotherBetsLibrary.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public DateTime TimeOfPrediction { get; set; }
        public virtual Bettor Bettor { get; set; }
        public virtual BetOption OutcomePredicted { get; set; }
    }
}