using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model
{
    public class Research
    {
        public Guid ResearchId { get; set; }        
        public Guid UserId { get; set; }
        
        public virtual List<DadosTrans> DadosTrans { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
