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
        public void AnalysisFileProcess(string file)
        {
            var data = Convert.FromBase64String(file);
            File.WriteAllBytes("../AnalysisFile.txt", data);
            var lines = File.ReadAllLines("../AnalysisFile.txt");

            var msp = 0;
            var msg = 0;
            var mg = 0;
            var total = 0;

            foreach(var wing in lines)
            {
                if (wing[0] == char.Parse("*") || !Regex.IsMatch(wing, @"[0-9]"))
                {
                    continue;
                }
                else
                {
                    //var sectors = wing.Split(" ");
                    var sectors = Regex.Matches(wing, @"[A-Z]'?(\d+)?-\d+");
                    foreach(Match sector in sectors)
                    {                        
                        var split = sector.ToString().Split("-");
                        var identifier = Regex.Match(split[0], @"\d+").Value;

                        if(string.IsNullOrWhiteSpace(identifier))
                        {
                            identifier = "1";
                        }

                        var postTrace = split[1];
                        if((int.Parse(identifier) <= 2 && int.Parse(postTrace) == 0) || (int.Parse(identifier) == 0 && (int.Parse(postTrace) <= 2)))
                        {
                            msp += 1;
                        }
                        else if((int.Parse(identifier) == 0 && int.Parse(postTrace) > 2) || (int.Parse(identifier) > 2 && int.Parse(postTrace) == 0))
                        {
                            msg += 1;
                        }
                        else //if(int.Parse(identifier) > 0 && int.Parse(postTrace) > 0)
                        {
                            mg += 1;
                        }                        
                    }
                        
                }                

            }
            total = msp + msg + mg;
        }


    }
}
