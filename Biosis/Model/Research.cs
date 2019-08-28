using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model
{
    public class Research
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResearchId { get; set; }        
        public Guid UserId { get; set; }
        
        public List<TransData> TransData { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
