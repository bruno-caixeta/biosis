using Biosis.DataObject;
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
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public Guid ControlId { get; set; }
        public Guid UserId { get; set; }
        
        public List<TransData> TransData { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Research()
        {

        }

        public Research(ResearchDTO researchDTO)
        {
            Description = researchDTO.Description;
            UserId = researchDTO.UserId;
            ControlId = researchDTO.ControlId;
        }

    }
}
