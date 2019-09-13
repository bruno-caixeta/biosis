using Biosis.DataObject;
using Biosis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Interface
{
    public interface IResearchBusinessLayer
    {
        Research CreateResearch(ResearchDTO researchDTO);
        Research GetResearch(Guid researchId);
    }
}
