using Biosis.BusinessLayer.Interface;
using Biosis.Model;
using Biosis.Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.BusinessLayer.Implementation
{
    public class TransDataBusinessLayer: ITransDataBusinessLayer
    {
        private readonly ITransDataRepository _transDataRepository;
        public TransDataBusinessLayer(ITransDataRepository transDataRepository)
        {
            _transDataRepository = transDataRepository;
        }
        public TransData GetTransData(Guid transDataId)
        {
            return _transDataRepository.GetOne(transDataId);                                    
        }
    }
}
