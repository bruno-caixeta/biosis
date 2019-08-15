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
        public int class1 { get; set; }
        public int class2 { get; set; }
        public int class3 { get; set; }
        public int class4 { get; set; }
        public int class5 { get; set; }
        public int class6 { get; set; }
        public int class7 { get; set; }
        public int class8 { get; set; }
        public int class9 { get; set; }
        public int class10 { get; set; }

        public int msp { get; set; }
        public int msg { get; set; }
        public int mg { get; set; }
        public int total { get; set; }
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
                    msp += 1;
                }
                else if (sector.Identifier == 0 && sector.PostTrace > 2 || sector.Identifier > 2 && sector.PostTrace == 0)
                {
                    msg += 1;
                }
                else //if(int.Parse(identifier) > 0 && int.Parse(postTrace) > 0)
                {
                    mg += 1;
                }
            }
            total = msp + msg + mg;
        }

        public void ClassifyByAlterationType(List<Sector> sectors)
        {
            foreach (Sector sector in sectors)
            {
                if (sector.Identifier == 1)
                {
                    class1 += 1;
                }
                else if (sector.Identifier == 2)
                {
                    class2 += 1;
                }
                else if (sector.Identifier > 2 && sector.Identifier <= 4)
                {
                    class3 += 1;
                }
                else if (sector.Identifier > 4 && sector.Identifier <= 8)
                {
                    class4 += 1;
                }
                else if (sector.Identifier > 8 && sector.Identifier <= 16)
                {
                    class5 += 1;
                }
                else if (sector.Identifier > 16 && sector.Identifier <= 32)
                {
                    class6 += 1;
                }
                else if (sector.Identifier > 32 && sector.Identifier <= 64)
                {
                    class7 += 1;
                }
                else if (sector.Identifier > 64 && sector.Identifier <= 128)
                {
                    class8 += 1;
                }
                else if (sector.Identifier > 128 && sector.Identifier <= 256)
                {
                    class9 += 1;
                }
                else if (sector.Identifier > 256)
                {
                    class10 += 1;
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
