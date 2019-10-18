using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model.Repository.Implementation
{
    public class TransDataRepository: GenericRepository<TransData>, ITransDataRepository
    {
        public List<TransData> GetControls()
        {
            using(var context = new DataContext())
            {
                var controls = context.TransData.Where(td => td.IsControl == true).ToList();
                return controls;
            }
        }
    }
}
