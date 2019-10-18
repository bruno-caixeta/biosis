using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis
{
    public class AnalysisFileDTO
    {
        public string Base64 { get; set; }
        public string Compound { get; set; }
        public string Dose { get; set; }
        public string Breed { get; set; }
        public bool IsControl { get; set; }
        public Guid? ResearchId { get; set; }
    }
}
