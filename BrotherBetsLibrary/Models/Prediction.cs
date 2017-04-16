using System;

namespace BrotherBetsLibrary.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public DateTime TimeOfPrediction { get; set; }
        public virtual Brother Brother { get; set; }
        public virtual BetOption OutcomePredicted { get; set; }
    }
}