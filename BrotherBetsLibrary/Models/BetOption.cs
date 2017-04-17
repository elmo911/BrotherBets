using System.ComponentModel.DataAnnotations;

namespace BrotherBetsLibrary.Models
{
    public class BetOption
    {
        public int Id { get; set; }
        [StringLength(75)]
        public string Outcome { get; set; }

        public bool Correct { get; set; }
        public virtual Bet Bet { get; set; }
    }
}