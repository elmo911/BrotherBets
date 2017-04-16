using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrotherBetsLibrary.Models
{
    public class Brother
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public virtual List<Prediction> Predictions { get; set; }
    }
}
