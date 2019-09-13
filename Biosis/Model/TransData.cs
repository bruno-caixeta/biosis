using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model
{
    public class TransData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransDataId { get; set; }
        public string Composto { get; set; }
        public string Cruzamento { get; set; }
        public string Dose { get; set; }
        public int NumeroIndividuos { get; set; }
        public int MSG { get; set; }
        public int MSP { get; set; }
        public int MG { get; set; }
        public int TotalManchas { get; set; }
        public bool IsControle { get; set; }
        public string DiagnosticoEstatistico { get; set; }
        public int Class1 { get; set; }
        public int Class2 { get; set; }
        public int Class3 { get; set; }
        public int Class4 { get; set; }
        public int Class5 { get; set; }
        public int Class6 { get; set; }
        public int Class7 { get; set; }
        public int Class8 { get; set; }
        public int Class9 { get; set; }
        public int Class10 { get; set; }
        public Guid? ResearchId { get; set; }

        [JsonIgnore]
        [ForeignKey("ResearchId")]
        public virtual Research Research { get; set; }
    }
}
