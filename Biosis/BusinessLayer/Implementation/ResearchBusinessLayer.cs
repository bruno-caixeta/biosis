using Biosis.BusinessLayer.Interface;
using Biosis.DataObject;
using Biosis.Model;
using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Implementation
{
    public class ResearchBusinessLayer: IResearchBusinessLayer
    {
        private readonly IResearchRepository _researchRepository;

        public ResearchBusinessLayer(IResearchRepository researchRepository)
        {
            _researchRepository = researchRepository;
        }

        public Research CreateResearch(ResearchDTO researchDTO)
        {
            var research = new Research(researchDTO);
            research.CreatedDate = DateTime.Now;
            return _researchRepository.Insert(research);
        }

        public Research GetResearch(Guid researchId)
        {
            return _researchRepository.GetOne(researchId);
        }

        public Research GetFullResearch(Guid researchId)
        {
            return _researchRepository.GetFullResearch(researchId);
        }
    }
}
