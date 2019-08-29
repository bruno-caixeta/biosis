using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis
{
    public class AnalysisFileDTO
    {
        public string Base64 { get; set; }
        public string Composto { get; set; }
        public string Dose { get; set; }
        public string Cruzamento { get; set; }
        public bool IsControle { get; set; }
    }
}
