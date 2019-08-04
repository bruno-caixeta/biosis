using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis
{
    public class AnalysisDataExtract
    {
        public void AnalysisFileProcess(string file)
        {
            var data = Convert.FromBase64String(file);
            File.WriteAllBytes("../AnalysisFile.txt", data);
            var lines = File.ReadAllLines("../AnalysisFile.txt");
        }
    }
}
