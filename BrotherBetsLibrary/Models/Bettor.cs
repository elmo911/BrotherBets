using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrotherBetsLibrary.Models
{
    public class Bettor
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(75)]
        public string Name { get; set; }
        public long Points { get; set; }
    }
}
