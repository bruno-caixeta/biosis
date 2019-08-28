using Biosis.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biosis
{
    public class AnalysisDataExtract
    {

        DadosTrans dadosTrans = new DadosTrans();
        
        public void ExtractValues(string file)
        {
            var data = Convert.FromBase64String(file);
            File.WriteAllBytes("../AnalysisFile.txt", data);
            var lines = File.ReadAllLines("../AnalysisFile.txt");

            List<Sector> sectorsData = new List<Sector>();

            foreach (var wing in lines)
            {
                if (wing[0] == char.Parse("*") || !Regex.IsMatch(wing, @"[0-9]"))
                {
                    continue;
                }
                else
                {
                    var sectors = Regex.Matches(wing, @"[A-Z]'?(\d+)?-(\d+)?");
                    foreach (Match sector in sectors)
                    {
                        var sectorData = new Sector();
                        var split = sector.ToString().Split("-");
                        var identifier = Regex.Match(split[0], @"\d+").Value;

                        if (string.IsNullOrWhiteSpace(identifier))
                        {
                            identifier = "1";
                        }

                        var postTrace = split[1];

                        if (string.IsNullOrWhiteSpace(postTrace))
                        {
                            postTrace = "1";
                        }

                        sectorData.Identifier = int.Parse(identifier);
                        sectorData.PostTrace = int.Parse(postTrace);
                        sectorsData.Add(sectorData);
                    }
                }
            }

            ClassifyBySize(sectorsData);
            ClassifyByAlterationType(sectorsData);

        }

        public void ClassifyBySize(List<Sector> sectors)
        {
            foreach (Sector sector in sectors)
            {                
                if (sector.Identifier <= 2 && sector.PostTrace == 0 || sector.Identifier == 0 && sector.PostTrace <= 2)
                {
                    dadosTrans.MSP += 1;
                }
                else if (sector.Identifier == 0 && sector.PostTrace > 2 || sector.Identifier > 2 && sector.PostTrace == 0)
                {
                    dadosTrans.MSG += 1;
                }
                else //if(int.Parse(identifier) > 0 && int.Parse(postTrace) > 0)
                {
                    dadosTrans.MG += 1;
                }
            }
            dadosTrans.TotalManchas = dadosTrans.MSP + dadosTrans.MSG + dadosTrans.MG;
        }

        public void ClassifyByAlterationType(List<Sector> sectors)
        {
            foreach (Sector sector in sectors)
            {
                if (sector.Identifier == 1)
                {
                    dadosTrans.Class1 += 1;
                }
                else if (sector.Identifier == 2)
                {
                    dadosTrans.Class2 += 1;
                }
                else if (sector.Identifier > 2 && sector.Identifier <= 4)
                {
                    dadosTrans.Class3 += 1;
                }
                else if (sector.Identifier > 4 && sector.Identifier <= 8)
                {
                    dadosTrans.Class4 += 1;
                }
                else if (sector.Identifier > 8 && sector.Identifier <= 16)
                {
                    dadosTrans.Class5 += 1;
                }
                else if (sector.Identifier > 16 && sector.Identifier <= 32)
                {
                    dadosTrans.Class6 += 1;
                }
                else if (sector.Identifier > 32 && sector.Identifier <= 64)
                {
                    dadosTrans.Class7 += 1;
                }
                else if (sector.Identifier > 64 && sector.Identifier <= 128)
                {
                    dadosTrans.Class8 += 1;
                }
                else if (sector.Identifier > 128 && sector.Identifier <= 256)
                {
                    dadosTrans.Class9 += 1;
                }
                else if (sector.Identifier > 256)
                {
                    dadosTrans.Class10 += 1;
                }
            }
        }
    }

    public class Sector
    {
        public int Identifier { get; set; }
        public int PostTrace { get; set; }
    }
}
