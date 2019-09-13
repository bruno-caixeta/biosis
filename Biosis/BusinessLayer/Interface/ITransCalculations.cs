using Biosis.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Interface
{
    public interface ITransCalculations
    {
        MemoryStream GeneratePdfReport(TransData controle);
    }
}
