using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Biosis.Model.Repository.Implementation
{
    public class ResearchRepository:GenericRepository<Research>, IResearchRepository
    {
        public Research GetFullResearch(Guid researchId)
        {
            using(var context = new DataContext())
            {
                return context.Research.Where(x => x.ResearchId == researchId).Include(x => x.TransData).FirstOrDefault();
            }
        }
    }
}
