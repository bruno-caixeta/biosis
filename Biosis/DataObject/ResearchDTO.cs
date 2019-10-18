using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.DataObject
{
    public class ResearchDTO
    {
        public string Description { get; set; }
        public Guid ControlId { get; set; }
        public Guid UserId { get; set; }
        public string Compound { get; set; }
    }
}
