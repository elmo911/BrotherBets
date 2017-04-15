using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrotherBetsLibrary.Models
{
    public class Bet
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime Expiration { get; set; }
        public virtual List<BetOption> BetOptions { get; set; }
    }
}
