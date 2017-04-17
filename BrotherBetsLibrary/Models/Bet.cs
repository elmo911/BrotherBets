using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BrotherBetsLibrary.Models
{
    public class Bet
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Prediction { get; set; }

        [DisplayName("Date Everyone Must Vote By")]
        public DateTime Expiration { get; set; }
        public bool Complete { get; set; }
        [Required]
        [DisplayName("Bet Creator")]
        public virtual Bettor Creator { get; set; }
        public virtual Bettor MarkedCompleteBy { get; set; }
        public virtual Brother Brother { get; set; }
        public virtual List<BetOption> BetOptions { get; set; }
    }
}
