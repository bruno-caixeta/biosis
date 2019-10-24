using Biosis.BusinessLayer.Implementation;
using Biosis.BusinessLayer.Interface;
using Biosis.Model;
using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biosis
{
    public class AnalysisDataExtract: IAnalysisDataExtract
    {
        private readonly ITransDataRepository _transDataRepository;
        TransData transData = new TransData();        

        public AnalysisDataExtract(ITransDataRepository transDataRepository)
        {
            _transDataRepository = transDataRepository;
        }
        
        public TransData ExtractValues(AnalysisFileDTO file)
        {
            transData.IsControl = file.IsControl;
            transData.ResearchId = file.ResearchId;
            transData.Compound = file.Compound;
            transData.Breed = file.Breed;
            transData.Dose = file.Dose;
            file.Base64 = file.Base64.Split(",")[1];
            var data = Convert.FromBase64String(file.Base64);
            File.WriteAllBytes("../AnalysisFile.txt", data);
            var lines = File.ReadAllLines("../AnalysisFile.txt");
            var population = 0;

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
                    population = population + 1;
                }
                
            }
            transData.PopulationNumber = population/2;

            ClassifyBySize(sectorsData);
            ClassifyByAlterationType(sectorsData);
            if (file.ResearchId != Guid.Empty)
            {
                transData.ResearchId = file.ResearchId;
            }
            
            return _transDataRepository.Insert(transData);
        }

        public void ClassifyBySize(List<Sector> sectors)
        {
            foreach (Sector sector in sectors)
            {                
                if (sector.Identifier <= 2 && sector.PostTrace == 0 || sector.Identifier == 0 && sector.PostTrace <= 2)
                {
                    transData.MSP += 1;
                }
                else if (sector.Identifier == 0 && sector.PostTrace > 2 || sector.Identifier > 2 && sector.PostTrace == 0)
                {
                    transData.MSG += 1;
                }
                else //if(int.Parse(identifier) > 0 && int.Parse(postTrace) > 0)
                {
                    transData.MG += 1;
                }
            }
            transData.TaintTotal = transData.MSP + transData.MSG + transData.MG;
        }

        public void ClassifyByAlterationType(List<Sector> sectors)
        {
            foreach (Sector sector in sectors)
            {
                if (sector.Identifier == 1)
                {
                    transData.Class1 += 1;
                }
                else if (sector.Identifier == 2)
                {
                    transData.Class2 += 1;
                }
                else if (sector.Identifier > 2 && sector.Identifier <= 4)
                {
                    transData.Class3 += 1;
                }
                else if (sector.Identifier > 4 && sector.Identifier <= 8)
                {
                    transData.Class4 += 1;
                }
                else if (sector.Identifier > 8 && sector.Identifier <= 16)
                {
                    transData.Class5 += 1;
                }
                else if (sector.Identifier > 16 && sector.Identifier <= 32)
                {
                    transData.Class6 += 1;
                }
                else if (sector.Identifier > 32 && sector.Identifier <= 64)
                {
                    transData.Class7 += 1;
                }
                else if (sector.Identifier > 64 && sector.Identifier <= 128)
                {
                    transData.Class8 += 1;
                }
                else if (sector.Identifier > 128 && sector.Identifier <= 256)
                {
                    transData.Class9 += 1;
                }
                else if (sector.Identifier > 256)
                {
                    transData.Class10 += 1;
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
