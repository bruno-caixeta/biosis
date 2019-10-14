﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biosis.Model.Repository.Interface
{
    public interface ITransDataRepository: IGenericRepository<TransData>
    {
        List<TransData> GetControls();
    }
}
